(function () {
    const searchBox = document.getElementById('search_input_box');
    const searchInput = document.getElementById('search_input');
    const searchResults = document.getElementById('search_results');
    const searchButton = document.getElementById('search');
    const closeSearch = document.getElementById('close_search');
    const clearSearch = document.getElementById('clear_search');

    if (!searchBox || !searchInput || !searchResults || !searchButton || !closeSearch || !clearSearch) {
        return;
    }

    const openSearch = _.throttle(() => {
        searchBox.classList.add('open');
        searchInput.focus();
    }, 200);

    const closeSearchPanel = _.throttle(() => {
        searchBox.classList.remove('open');
        searchInput.value = '';
        searchResults.hidden = true;
        searchResults.innerHTML = '';
    }, 200);

    const fetchSearchResults = _.debounce(function () {
        const keyword = searchInput.value.trim();

        if (!keyword) {
            searchResults.innerHTML = '';
            searchResults.hidden = true;
            return;
        }

        searchResults.hidden = false;
        searchResults.innerHTML = '<div class="search-empty">Đang tìm kiếm...</div>';

        fetch(`/Products/Search?q=${encodeURIComponent(keyword)}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Search failed');
                }

                return response.text();
            })
            .then(html => {
                searchResults.innerHTML = html;
                searchResults.hidden = false;
            })
            .catch(() => {
                searchResults.innerHTML = '<div class="search-empty">Không thể tải kết quả tìm kiếm.</div>';
                searchResults.hidden = false;
            });
    }, 350);

    searchButton.addEventListener('click', openSearch);
    closeSearch.addEventListener('click', closeSearchPanel);
    clearSearch.addEventListener('click', () => {
        searchInput.value = '';
        searchResults.innerHTML = '';
        searchResults.hidden = true;
        searchInput.focus();
    });

    searchInput.addEventListener('input', fetchSearchResults);

    document.addEventListener('keydown', (event) => {
        if (event.key === 'Escape' && searchBox.classList.contains('open')) {
            closeSearchPanel();
        }
    });

    closeSearch.addEventListener('keydown', (event) => {
        if (event.key === 'Enter' || event.key === ' ') {
            event.preventDefault();
            closeSearchPanel();
        }
    });
})();
