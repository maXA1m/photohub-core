const search = new Vue({
    el: '#blocklist',
    data: {
        currentAppUserName: document.querySelector('#body').dataset.appUser,
        users: null,

        usersLoaded: false,
        incallback: false,
        page: 0,

        message: {
            text: null,
            status: null
        }
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
                nanobar.go(40);

                this.$http.get(`/api/users/blocklist/${this.page}`).then(response => response.json()).then(json => {

                    if (this.users == null)
                        this.users = json;
                    else if (json.length == 0)
                        this.usersLoaded = true;
                    else {
                        for (let user in json)
                            this.users.push(json[user]);
                    }

                    nanobar.go(100);
                    this.incallback = false;
                    this.page++;
                },
                    error => {
                        nanobar.go(0);
                        this.incallback = false;
                        this.message.text = 'error while fetching users';
                        this.message.status = 'error';
                    });
            }
        },
        autoLoad() {
            if (!this.postsLoaded && this.users.length % 12 == 0 && document.documentElement.scrollTop == document.documentElement.scrollHeight - window.innerHeight)
                this.fetchUsers();
        },

        block(user) {
            if (!this.currentAppUserName)
                return -1;

            if (!user.blocked) {
                user.blocked = true;

                this.$http.post(`/api/users/block/${user.userName}`).then(response => {

                }, response => {
                    this.message.text = 'error while blocking';
                    this.message.status = 'error';

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
                    this.message.text = 'error while disblocking';
                    this.message.status = 'error';

                    user.blocked = true;
                });
            }
        },
    }
});