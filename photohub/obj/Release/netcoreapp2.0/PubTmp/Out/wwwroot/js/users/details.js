const userDetails = new Vue({
    el: '#user',
    data: {
        currentAppUserName: document.querySelector('#body').dataset.appUser,
        detailsUserName: document.querySelector('#user').dataset.user,

        posts: null,
        user: null,
        giveaways: null,

        postsFetch: {
            page: 0,
            incallback: false,
            loaded: false,
        },
        giveawaysFetch: {
            page: 0,
            incallback: false,
            loaded: false,
        },

        current: null,
        commenting: false,
        message: {
            status: null,
            text: null
        },
        modals: {
            likeActive: false,
            commentActive: false,
            optionActive: false,

            followings: false,
            followers: false
        }
    },
    created() {
        this.fetchUser();
        this.fetchGiveaways();
        this.fetchPhotos();
    },
    mounted() {
        window.addEventListener('scroll', this.autoFetchPhotos);
    },
    methods: {
        fetchGiveaways() {
            if (!this.giveawaysFetch.incallback && this.giveawaysFetch.page > -1) {

                this.giveawaysFetch.incallback = true;

                this.$http.get(`/api/giveaways/${this.detailsUserName}/${this.giveawaysFetch.page}`).then(response => response.json()).then(json => {

                    if (this.giveaways == null)
                        this.giveaways = json;
                    else if (json.length == 0)
                        this.giveawaysFetch.loaded = true;
                    else {
                        for (let giveaway in json)
                            this.giveaways.push(json[giveaway]);
                    }
                    
                    this.giveawaysFetch.incallback = false;
                    this.giveawaysFetch.page++;
                },
                error => {
                    this.giveawaysFetch.incallback = false;
                    this.message.text = 'error while fetching photos';
                    this.message.status = 'error';
                });
            }
        },
        fetchUser() {
            this.$http.get(`/api/users/details/${this.detailsUserName}`).then(response => response.json()).then(json => {
                this.user = json;
            },
            error => {
                this.message.text = 'error while fetching user';
                this.message.status = 'error';
            });
        },
        fetchPhotos() {
            if (!this.postsFetch.incallback && this.postsFetch.page > -1) {

                this.postsFetch.incallback = true;
                nanobar.go(40);

                this.$http.get(`/api/photos/user/${this.detailsUserName}/${this.postsFetch.page}`).then(response => response.json()).then(json => {

                    if (this.posts == null)
                        this.posts = json;
                    else if (json.length == 0)
                        this.postsFetch.loaded = true;
                    else {
                        for (let post in json)
                            this.posts.push(json[post]);
                    }

                    nanobar.go(100);
                    this.postsFetch.incallback = false;
                    this.postsFetch.page++;
                },
                error => {
                    nanobar.go(0);
                    this.postsFetch.incallback = false;
                    this.message.text = 'error while fetching photos';
                    this.message.status = 'error';
                });
            }
        },

        like(post) {
            if (!this.currentAppUserName)
                return -1;

            if (!post.liked) {
                post.liked = true;
                post.likes.push({
                    date: this.getCurrentDate(),
                    owner: {
                        userName: this.currentAppUserName
                    }
                });
                this.$http.post(`/api/likes/add/${post.$id}`).then(response => {

                }, response => {
                    this.message.text = 'error while sending like';
                    this.message.status = 'error';

                    post.liked = false;
                    for (let i in post.likes) {
                        if (post.likes[i].owner.userName == this.currentAppUserName)
                            post.likes.splice(i, 1);
                    }
                });
            }
        },
        dislike(post) {
            if (!this.currentAppUserName)
                return -1;

            if (post.liked) {
                post.liked = false;
                for (let i in post.likes) {
                    if (post.likes[i].owner.userName == this.currentAppUserName)
                        post.likes.splice(i, 1);
                }
                this.$http.post(`/api/likes/delete/${post.$id}`).then(response => {

                }, response => {
                    this.message.text = 'error while sending like';
                    this.message.status = 'error';

                    post.liked = true;
                    post.likes.push({
                        date: this.getCurrentDate(),
                        owner: {
                            userName: this.currentAppUserName
                        }
                    });
                });
            }
        },
        comment() {
            if (!this.currentAppUserName)
                return -1;

            const text = document.querySelector('#comment').value;

            if (text == '' || text.length < 1)
                return -1;

            this.commenting = true;
            nanobar.go(40);

            const words = text.split(/[, ;.]/);

            let ok = true;

            for (let word in words) {
                if (words[word].length > 12)
                    ok = false;
            }

            if (ok) {
                this.$http.post(`/api/comments/add?photoId=${this.current.$id}&text=${text}`).then(response => response.json()).then(json => {
                    this.current.comments.push({
                        $id: json,
                        text: text,
                        date: this.getCurrentDate(),
                        owner: {
                            userName: this.currentAppUserName
                        }
                    });

                    document.querySelector('#comment').value = '';
                    this.commenting = false;
                    nanobar.go(100);
                }, response => {
                    this.message.text = 'error while sending comment';
                    this.message.status = 'error';
                    this.commenting = false;
                    nanobar.go(0);
                });
            }
            else {
                nanobar.go(0);
                this.commenting = false;
            }
        },
        deleteComment(comment) {
            if (comment.owner.userName == this.currentAppUserName) {
                nanobar.go(60);
                this.$http.post(`/api/comments/delete/${comment.$id}`).then(response => {
                    for (let i in this.current.comments) {
                        if (this.current.comments[i].$id == comment.$id)
                            this.current.comments.splice(i, 1);
                    }
                    nanobar.go(100);
                }, response => {
                    nanobar.go(0);
                    this.message.text = 'error while deleting comment';
                    this.message.status = 'error';
                });
            }
        },
        copyToClipboard(id) {
            const copyTextArea = document.createElement('textarea');
            copyTextArea.value = `http://photohub.azurewebsites.net/Photos/${id}`;
            document.body.appendChild(copyTextArea);
            copyTextArea.select();

            const successful = document.execCommand('copy');

            document.body.removeChild(copyTextArea);
            this.closeOptions();
        },
        autoFetchPhotos() {
            if (!this.postsLoaded && this.posts.length % 6 == 0 && document.documentElement.scrollTop == document.documentElement.scrollHeight - window.innerHeight)
                this.fetchPhotos();
        },

        follow() {
            if (!this.currentAppUserName)
                return -1;

            if (!this.user.followed) {
                this.user.followed = true;

                this.$http.post(`/api/users/follow/${this.user.userName}`).then(response => {

                }, response => {
                    this.message.text = 'error while following';
                    this.message.status = 'error';

                    this.user.followed = false;
                });
            }
        },
        disfollow() {
            if (!this.currentAppUserName)
                return -1;

            if (this.user.followed) {
                this.user.followed = false;

                this.$http.post(`/api/users/dismiss/follow/${this.user.userName}`).then(response => {

                }, response => {
                    this.message.text = 'error while disfollowing';
                    this.message.status = 'error';

                    this.user.followed = true;
                });
            }
        },
        block() {
            if (!this.currentAppUserName)
                return -1;

            if (!this.user.blocked) {
                this.user.blocked = true;

                this.$http.post(`/api/users/block/${this.user.userName}`).then(response => {

                }, response => {
                    this.message.text = 'error while blocking';
                    this.message.status = 'error';

                    this.user.blocked = false;
                });
            }
        },
        disblock() {
            if (!this.currentAppUserName)
                return -1;

            if (this.user.blocked) {
                this.user.blocked = false;

                this.$http.post(`/api/users/dismiss/block/${this.user.userName}`).then(response => {

                }, response => {
                    this.message.text = 'error while disblocking';
                    this.message.status = 'error';

                    this.user.blocked = true;
                });
            }
        },

        showFollowings() {
            this.modals.followings = true;
        },
        showFollowers() {
            this.modals.followers = true;
        },
        closeFollowings() {
            this.modals.followings = false;
        },
        closeFollowers() {
            this.modals.followers = false;
        },

        showLikes(post) {
            this.modals.likeActive = true;
            this.current = post;
        },
        showComments(post) {
            this.modals.commentActive = true;
            this.current = post;
        },
        showOptions(post) {
            this.modals.optionActive = true;
            this.current = post;
        },
        closeLikes() {
            this.modals.likeActive = false;
        },
        closeComments() {
            this.modals.commentActive = false;
        },
        closeOptions() {
            this.modals.optionActive = false;
        },

        getCurrentDate() {
            const currentdate = new Date();
            const datetime = currentdate.getDay() + '-' + currentdate.getMonth() + '-' + currentdate.getFullYear() + " "
                + (currentdate.getHours() > 9 ? currentdate.getHours() : "0" + currentdate.getHours()) + ":" + (currentdate.getMinutes() > 9 ? currentdate.getMinutes() : "0" + currentdate.getMinutes());

            return datetime;
        }
    }
});