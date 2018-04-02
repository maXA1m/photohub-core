const search = new Vue({
    el: '#search',
    data: {
        currentAppUserName: document.querySelector('#body').dataset.appUser,
        current: null,
        commenting: false,

        iso: 0,
        exposure: 0,
        aperture: 0,
        tags: '',

        photosTab: {
            search: '',
            active: false,
            photos: [],
            loaded: false,
            incallback: false,
            page: 0
        },
        usersTab: {
            search: '',
            active: true,
            users: [],
            loaded: false,
            incallback: false,
            page: 0
        },
        modals: {
            likeActive: false,
            commentActive: false,
            optionActive: false,
            metadataActive: false
        },
        message: {
            text: null,
            status: null
        }
    },
    mounted() {
        window.addEventListener('scroll', this.autoSearch);
    },
    created() {
        this.search();
    },
    methods: {
        search() {
            if (this.usersTab.active && !this.usersTab.incallback && this.usersTab.page > -1 && !this.usersTab.loaded) {

                this.usersTab.incallback = true;
                nanobar.go(40);

                this.$http.get(`/api/users/search?search=${this.usersTab.search}&page=${this.usersTab.page}`).then(response => response.json()).then(json => {

                    if (this.usersTab.users == null)
                        this.usersTab.users = json;
                    else if (json.length == 0)
                        this.usersTab.loaded = true;
                    else {
                        for (let user in json)
                            this.usersTab.users.push(json[user]);
                    }

                    nanobar.go(100);
                    this.usersTab.incallback = false;
                    this.usersTab.page++;
                },
                error => {
                    nanobar.go(0);
                    this.usersTab.incallback = false;
                    this.message.text = 'error while fetching users';
                    this.message.status = 'error';
                });
            }
            else if (this.photosTab.active && !this.photosTab.incallback && this.photosTab.page > -1 && !this.photosTab.loaded) {
                this.photosTab.incallback = true;
                nanobar.go(40);

                this.$http.get(`/api/photos/search?search=${this.photosTab.search}&page=${this.photosTab.page}&iso=${this.iso}&exposure=${this.exposure}&aperture=${this.aperture}`).then(response => response.json()).then(json => {

                    if (this.photosTab.photos == null)
                        this.photosTab.photos = json;
                    else if (json.length == 0)
                        this.photosTab.loaded = true;
                    else {
                        for (let photo in json)
                            this.photosTab.photos.push(json[photo]);
                    }

                    nanobar.go(100);
                    this.photosTab.incallback = false;
                    this.photosTab.page++;
                },
                    error => {
                        nanobar.go(0);
                        this.photosTab.incallback = false;
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
                    this.message.text = 'error while sending dislike';
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

            const words = text.split(/[, ;.]/);;

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
            copyTextArea.value = `https://photohub.azurewebsites.net/photos/${id}`;
            document.body.appendChild(copyTextArea);
            copyTextArea.select();

            const successful = document.execCommand('copy');

            document.body.removeChild(copyTextArea);
            this.closeOptions();
        },
        autoSearch() {
            const scrollTop = document.documentElement.scrollTop;
            const windowHeight = window.innerHeight;
            let bodyHeight = document.documentElement.scrollHeight - windowHeight;
            let scrollPercentage = (scrollTop / bodyHeight);

            // If the scroll is more than 60% from the top, load more content. Load users
            if (this.usersTab.active && !this.usersTab.loaded && this.usersTab.users.length % 12 == 0 && scrollPercentage > 0.6)
                this.search();
            else if (this.photosTab.active && !this.photosTab.loaded && this.photosTab.photos.length % 8 == 0 && scrollPercentage > 0.6)
                this.search();
        },
        handleSearch(str) {
            if (this.usersTab.active && (this.usersTab.search.length > 2 || !this.usersTab.search.length)) {
                this.usersTab.users = null;
                this.usersTab.page = 0;
                this.usersTab.loaded = false;
                this.search();
            }
            else if (this.photosTab.active && (this.photosTab.search.length > 2 || !this.photosTab.search.length)) {
                this.photosTab.photos = null;
                this.photosTab.page = 0;
                this.photosTab.loaded = false;
                this.search();
            }
        },

        follow(user) {
            if (!this.currentAppUserName)
                return -1;

            if (!user.followed) {
                user.followed = true;
                this.$http.post(`/api/users/follow/${user.userName}`).then(response => {

                }, response => {
                    this.message.text = 'error while following';
                    this.message.status = 'error';

                    user.followed = false;
                });
            }
        },
        disfollow(user) {
            if (!this.currentAppUserName)
                return -1;

            if (user.followed) {
                user.followed = false;

                this.$http.post(`/api/users/dismiss/follow/${user.userName}`).then(response => {

                }, response => {
                    this.message.text = 'error while disfollowing';
                    this.message.status = 'error';

                    user.followed = true;
                });
            }
        },

        bookmark(post) {
            if (!this.currentAppUserName)
                return -1;

            if (!post.bookmarked) {
                post.bookmarked = true;

                this.$http.post(`/api/photos/bookmark/${post.$id}`).then(response => {

                }, response => {
                    this.message.text = 'error while bookmarking';
                    this.message.status = 'error';

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

                }, response => {
                    this.message.text = 'error while dismising bookmark';
                    this.message.status = 'error';

                    post.bookmarked = true;
                });
            }
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

        showUsers() {
            this.usersTab.active = true;
            this.photosTab.active = false;

            if(!this.usersTab.users)
                this.handleSearch();
        },
        showPhotos() {
            this.usersTab.active = false;
            this.photosTab.active = true;

            if (!this.photosTab.photos)
                this.handleSearch();
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