(() => {
    const root = document.getElementById('support-chat-widget');
    if (!root || !window.signalR) return;

    const toggle = document.getElementById('support-chat-toggle');
    const close = document.getElementById('support-chat-close');
    const panel = document.getElementById('support-chat-panel');
    const form = document.getElementById('support-chat-form');
    const input = document.getElementById('support-chat-input');
    const messages = document.getElementById('support-chat-messages');
    const status = document.getElementById('support-chat-status');
    let loaded = false;

    const connection = new signalR.HubConnectionBuilder()
        .withUrl(root.dataset.hubUrl)
        .withAutomaticReconnect()
        .build();

    function appendMessage(message) {
        const item = document.createElement('div');
        item.className = `support-chat-message ${message.senderType === 'Customer' ? 'customer' : 'shop'}`;
        const text = document.createElement('span');
        text.textContent = message.content;
        const time = document.createElement('small');
        time.className = 'support-chat-time';
        time.textContent = new Date(message.sentAtUtc).toLocaleString();
        item.append(text, time);
        messages.appendChild(item);
        messages.scrollTop = messages.scrollHeight;
    }

    connection.on('MessageReceived', appendMessage);
    connection.onreconnecting(() => status.textContent = 'Đang kết nối lại...');
    connection.onreconnected(async () => {
        status.textContent = 'Đã kết nối';
        loaded = false;
        if (!panel.hidden) await loadHistory();
    });
    connection.onclose(() => status.textContent = 'Mất kết nối. Vui lòng tải lại trang.');

    async function loadHistory() {
        if (loaded) return;
        try {
            if (connection.state === signalR.HubConnectionState.Disconnected) await connection.start();
            const history = await connection.invoke('GetCustomerHistory');
            messages.replaceChildren();
            history.forEach(appendMessage);
            loaded = true;
            status.textContent = 'Đã kết nối';
        } catch {
            status.textContent = 'Không thể kết nối. Hãy đăng nhập lại hoặc thử lại sau.';
        }
    }

    async function openChat() {
        panel.hidden = false;
        toggle.setAttribute('aria-expanded', 'true');
        await loadHistory();
    }

    toggle.addEventListener('click', openChat);
    close.addEventListener('click', () => {
        panel.hidden = true;
        toggle.setAttribute('aria-expanded', 'false');
    });

    form.addEventListener('submit', async event => {
        event.preventDefault();
        const content = input.value.trim();
        if (!content) return;
        try {
            if (connection.state === signalR.HubConnectionState.Disconnected) await connection.start();
            await connection.invoke('SendCustomerMessage', content);
            input.value = '';
        } catch {
            status.textContent = 'Không gửi được tin nhắn. Vui lòng thử lại.';
        }
    });
})();
