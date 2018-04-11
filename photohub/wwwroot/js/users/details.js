const userDetails = new Vue({
    el: '#user',
    data: {
        currentAppUserName: document.querySelector('#body').dataset.appUser,
        detailsUserName: document.querySelector('#user').dataset.user,
        preloader: document.querySelector('#preloader'),
        message: {
            element: document.querySelector('#message'),
            text: document.querySelector('#message .message-text')
        },

        posts: [],
        user: null,

        postsFetch: {
            page: 0,
            incallback: false,
            loaded: false,
        },

        current: null,
        commenting: false,
        mutualsActive: false,
        mutuals: [],
        modals: {
            likeActive: false,
            commentActive: false,
            optionActive: false,
            metadataActive: false,

            followings: false,
            followers: false
        }
    },
    created() {
        this.fetchUser();
        this.fetchPhotos();
    },
    mounted() {
        window.addEventListener('scroll', this.autoFetchPhotos);
    },
    methods: {
        fetchUser() {
            this.$http.get(`/api/users/details/${this.detailsUserName}`).then(response => response.json()).then(json => {
                this.user = json;
                if (this.user.mutuals != null && this.user.mutuals.length)
                    this.mutuals = this.user.mutuals.slice(0, 3);
            },
                error => {
                    this.message.text.innerHTML = 'Error while loading user';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);
            });
        },
        fetchPhotos() {
            if (!this.postsFetch.incallback && this.postsFetch.page > -1 && !this.postsFetch.loaded) {

                this.postsFetch.incallback = true;
                this.preloader.setAttribute('data-hidden', 'false');

                this.$http.get(`/api/photos/user/${this.detailsUserName}/${this.postsFetch.page}`).then(response => response.json()).then(json => {

                    if (this.posts == null)
                        this.posts = json;
                    else if (json.length == 0)
                        this.postsFetch.loaded = true;
                    else {
                        for (let post in json)
                            this.posts.push(json[post]);
                    }

                    this.preloader.setAttribute('data-hidden', 'true');
                    this.postsFetch.incallback = false;
                    this.postsFetch.page++;
                },
                error => {
                    this.preloader.setAttribute('data-hidden', 'true');
                    this.postsFetch.incallback = false;
                    this.message.text.innerHTML = 'Error while loading photos';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);
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
                    this.message.text.innerHTML = 'Error while liking photo';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

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
                    this.message.text.innerHTML = 'Error while disliking photo';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

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
            this.preloader.setAttribute('data-hidden', 'false');

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
                    this.preloader.setAttribute('data-hidden', 'true');
                }, response => {
                    this.message.text.innerHTML = 'Error while commenting photo';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);
                    this.commenting = false;
                    this.preloader.setAttribute('data-hidden', 'true');
                });
            }
            else {
                this.preloader.setAttribute('data-hidden', 'true');
                this.commenting = false;
            }
        },
        deleteComment(comment) {
            if (comment.owner.userName == this.currentAppUserName) {
                this.preloader.setAttribute('data-hidden', 'false');
                this.$http.post(`/api/comments/delete/${comment.$id}`).then(response => {
                    for (let i in this.current.comments) {
                        if (this.current.comments[i].$id == comment.$id)
                            this.current.comments.splice(i, 1);
                    }
                    this.preloader.setAttribute('data-hidden', 'true');
                }, response => {
                    this.preloader.setAttribute('data-hidden', 'true');
                    this.message.text.innerHTML = 'Error while deleting comment';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);
                });
            }
        },
        copyToClipboard(id) {
            const copyTextArea = document.createElement('textarea');
            copyTextArea.value = `https://${window.location.hostname}/photos/${id}`;
            document.body.appendChild(copyTextArea);
            copyTextArea.select();

            const successful = document.execCommand('copy');

            document.body.removeChild(copyTextArea);
            this.closeOptions();

            this.message.text.innerHTML = 'Link copied to clipboard';
            this.message.element.setAttribute('data-message-type', 'success');
            this.message.element.setAttribute('data-hidden', 'false');
            setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 3000);
        },
        autoFetchPhotos() {
            const scrollTop = document.documentElement.scrollTop;
            const windowHeight = window.innerHeight;
            let bodyHeight = document.documentElement.scrollHeight - windowHeight;
            let scrollPercentage = (scrollTop / bodyHeight);

            // If the scroll is more than 60% from the top, load more content. Load photos
            if (!this.postsFetch.loaded && this.posts.length % 4 == 0 && scrollPercentage > 0.6)
                this.fetchPhotos();
        },

        follow() {
            if (!this.currentAppUserName)
                return -1;

            if (!this.user.followed) {
                this.user.followed = true;

                this.$http.post(`/api/users/follow/${this.user.userName}`).then(response => {

                }, response => {
                    this.message.text.innerHTML = 'Error while following';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

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
                    this.message.text.innerHTML = 'Error while unfollowing';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

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
                    this.message.text.innerHTML = 'Error while blocking';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

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
                    this.message.text.innerHTML = 'Error while unblocking';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

                    this.user.blocked = true;
                });
            }
        },

        bookmark(post) {
            if (!this.currentAppUserName)
                return -1;

            if (!post.bookmarked) {
                post.bookmarked = true;

                this.$http.post(`/api/photos/bookmark/${post.$id}`).then(response => {
                    this.message.text.innerHTML = 'Saved to bookmarks';
                    this.message.element.setAttribute('data-message-type', 'success');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 3000);
                }, response => {
                    this.message.text.innerHTML = 'Error while bookmarking';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

                    post.bookmarked = false;
                });
            }
        },
        disbookmark(post) {
            if (!this.currentAppUserName)
                return -1;

            if (post.bookmarked) {
                post.bookmarked = false;

                this.$http.post(`/api/photos/dismiss/bookmark/${post.$id}`).then(response => {
                    this.message.text.innerHTML = 'Removed from bookmarks';
                    this.message.element.setAttribute('data-message-type', 'success');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 3000);
                }, response => {
                    this.message.text.innerHTML = 'Error while deleting bookmark';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

                    post.bookmarked = true;
                });
            }
        },

        showMutuals() {
            this.mutuals = this.user.mutuals;
            this.mutualsActive = true;
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
        closeMutuals() {
            this.mutuals = this.user.mutuals.slice(0, 3);
            this.mutualsActive = false;
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
        showMetadata(post) {
            this.modals.metadataActive = true;
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
        closeMetadata() {
            this.modals.metadataActive = false;
        },

        getCurrentDate() {
            const date = new Date();

            const monthNames = [
                "January", "February", "March",
                "April", "May", "June", "July",
                "August", "September", "October",
                "November", "December"
            ];

            const day = date.getDate();
            const monthIndex = date.getMonth();
            const year = date.getFullYear();

            return `${monthNames[monthIndex]} ${day}, ${year}`;
        }
    }
});