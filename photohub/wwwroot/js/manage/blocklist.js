const blocklist = new Vue({
    el: '#blocklist',
    data: {
        currentAppUserName: document.querySelector('#body').dataset.appUser,
        preloader: document.querySelector('#preloader'),
        message: {
            element: document.querySelector('#message'),
            text: document.querySelector('#message .message-text')
        },
        users: [],

        usersLoaded: false,
        incallback: false,
        page: 0
    },
    mounted() {
        window.addEventListener('scroll', this.autoLoad);
    },
    created() {
        this.fetchUsers();
    },
    methods: {
        fetchUsers() {
            if (!this.incallback && this.page > -1 && !this.usersLoaded) {

                this.incallback = true;
                this.preloader.setAttribute('data-hidden', 'false');

                this.$http.get(`/api/users/blocklist/${this.page}`).then(response => response.json()).then(json => {

                    if (this.users == null)
                        this.users = json;
                    else if (json.length == 0)
                        this.usersLoaded = true;
                    else {
                        for (let user in json)
                            this.users.push(json[user]);
                    }

                    this.preloader.setAttribute('data-hidden', 'true');
                    this.incallback = false;
                    this.page++;
                },
                    error => {
                        this.preloader.setAttribute('data-hidden', 'true');
                        this.incallback = false;
                        this.message.text.innerHTML = 'Error while loading users';
                        this.message.element.setAttribute('data-message-type', 'error');
                        this.message.element.setAttribute('data-hidden', 'false');
                        setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);
                    });
            }
        },
        autoLoad() {
            const scrollTop = document.documentElement.scrollTop;
            const windowHeight = window.innerHeight;
            let bodyHeight = document.documentElement.scrollHeight - windowHeight;
            let scrollPercentage = (scrollTop / bodyHeight);

            // If the scroll is more than 60% from the top, load more content. Load users
            if (!this.usersLoaded && this.users.length % 12 == 0 && scrollPercentage > 0.6)
                this.fetchUsers();
        },

        block(user) {
            if (!this.currentAppUserName)
                return -1;

            if (!user.blocked) {
                user.blocked = true;

                this.$http.post(`/api/users/block/${user.userName}`).then(response => {

                }, response => {
                    this.message.text.innerHTML = 'Error while blocking user';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

                    user.blocked = false;
                });
            }
        },
        disblock(user) {
            if (!this.currentAppUserName)
                return -1;

            if (user.blocked) {
                user.blocked = false;

                this.$http.post(`/api/users/dismiss/block/${user.userName}`).then(response => {

                }, response => {
                    this.message.text.innerHTML = 'Error while unblocking user';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

                    user.blocked = true;
                });
            }
        },
    }
});