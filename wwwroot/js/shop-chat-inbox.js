(() => {
    const root = document.getElementById('shop-chat-inbox');
    if (!root || !window.signalR) return;

    const list = document.getElementById('shop-chat-list');
    const active = document.getElementById('shop-chat-active');
    const empty = document.getElementById('shop-chat-empty');
    const heading = document.getElementById('shop-chat-customer');
    const messages = document.getElementById('shop-chat-messages');
    const form = document.getElementById('shop-chat-form');
    const input = document.getElementById('shop-chat-input');
    const status = document.getElementById('shop-chat-status');
    let conversations = [];
    let currentId = null;

    const connection = new signalR.HubConnectionBuilder()
        .withUrl(root.dataset.hubUrl)
        .withAutomaticReconnect()
        .build();

    function appendMessage(message) {
        if (message.conversationId !== currentId) return;
        const item = document.createElement('div');
        item.className = `support-chat-message ${message.senderType === 'Shop' ? 'customer' : 'shop'}`;
        const text = document.createElement('span');
        text.textContent = message.content;
        const meta = document.createElement('small');
        meta.className = 'support-chat-time';
        meta.textContent = `${message.senderName} · ${new Date(message.sentAtUtc).toLocaleString()}`;
        item.append(text, meta);
        messages.appendChild(item);
        messages.scrollTop = messages.scrollHeight;
    }

    function renderList() {
        list.replaceChildren();
        for (const conversation of conversations) {
            const button = document.createElement('button');
            button.type = 'button';
            button.className = `shop-chat-conversation ${conversation.id === currentId ? 'active' : ''}`;
            const name = document.createElement('strong');
            name.textContent = conversation.customerName;
            if (conversation.unreadCount > 0) {
                const unread = document.createElement('span');
                unread.className = 'shop-chat-unread';
                unread.textContent = String(conversation.unreadCount);
                name.append(' ', unread);
            }
            const email = document.createElement('small');
            email.textContent = conversation.email || '';
            const preview = document.createElement('span');
            preview.className = 'shop-chat-preview';
            preview.textContent = conversation.lastMessage || 'Chưa có tin nhắn';
            button.append(name, email, preview);
            button.addEventListener('click', () => openConversation(conversation));
            list.appendChild(button);
        }
    }

    async function refreshInbox() {
        try {
            conversations = await connection.invoke('GetInbox');
            renderList();
        } catch {
            status.textContent = 'Không tải được hộp thư. Vui lòng tải lại trang.';
        }
    }

    async function openConversation(conversation) {
        currentId = conversation.id;
        heading.textContent = `${conversation.customerName}${conversation.email ? ` · ${conversation.email}` : ''}`;
        messages.replaceChildren();
        empty.hidden = true;
        active.hidden = false;
        renderList();
        try {
            const history = await connection.invoke('GetConversationHistory', currentId);
            history.forEach(appendMessage);
            await refreshInbox();
        } catch {
            status.textContent = 'Không mở được cuộc trò chuyện.';
        }
    }

    connection.on('MessageReceived', message => {
        appendMessage(message);
        refreshInbox();
    });
    connection.on('ConversationUpdated', refreshInbox);
    connection.onreconnecting(() => status.textContent = 'Đang kết nối lại...');
    connection.onreconnected(async () => {
        status.textContent = 'Đã kết nối';
        await refreshInbox();
        const selected = conversations.find(c => c.id === currentId);
        if (selected) await openConversation(selected);
    });

    async function start() {
        try {
            await connection.start();
            status.textContent = 'Đã kết nối';
            await refreshInbox();
        } catch {
            status.textContent = 'Không thể kết nối hộp thư. Hãy tải lại trang.';
        }
    }

    document.getElementById('shop-chat-refresh').addEventListener('click', refreshInbox);
    form.addEventListener('submit', async event => {
        event.preventDefault();
        const content = input.value.trim();
        if (!content || currentId === null) return;
        try {
            await connection.invoke('SendShopMessage', currentId, content);
            input.value = '';
        } catch {
            status.textContent = 'Không gửi được tin nhắn. Vui lòng thử lại.';
        }
    });

    start();
})();
