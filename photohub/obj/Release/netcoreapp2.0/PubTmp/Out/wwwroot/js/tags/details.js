const tagsDeatils = new Vue({
    el: '#posts',
    data: {
        currentAppUserName: document.querySelector('#body').dataset.appUser,
        currentTagName: document.querySelector('#posts').dataset.curTag,
        posts: [],

        page: 0,
        incallback: false,
        postsLoaded: false,

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
            metadataActive: false
        }
    },
    created() {
        this.fetchPhotos();
    },
    mounted() {
        window.addEventListener('scroll', this.autoFetchPhotos);
    },
    methods: {
        fetchPhotos() {
            if (!this.incallback && this.page > -1 && !this.postsLoaded) {

                this.incallback = true;
                nanobar.go(40);

                this.$http.get(`/api/photos/tags/${this.currentTagName}/${this.page}`).then(response => response.json()).then(json => {

                    if (this.posts == null)
                        this.posts = json;
                    else if (json.length == 0)
                        this.postsLoaded = true;
                    else {
                        for (let post in json)
                            this.posts.push(json[post]);
                    }

                    nanobar.go(100);
                    this.incallback = false;
                    this.page++;
                },
                    error => {
                        nanobar.go(0);
                        this.incallback = false;
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
            copyTextArea.value = `https://${window.location.hostname}/photos/${id}`;
            document.body.appendChild(copyTextArea);
            copyTextArea.select();

            const successful = document.execCommand('copy');

            document.body.removeChild(copyTextArea);
            this.closeOptions();
        },
        autoFetchPhotos() {
            const scrollTop = document.documentElement.scrollTop;
            const windowHeight = window.innerHeight;
            let bodyHeight = document.documentElement.scrollHeight - windowHeight;
            let scrollPercentage = (scrollTop / bodyHeight);

            // If the scroll is more than 60% from the top, load more content. Load photos
            if (!this.postsLoaded && this.posts.length % 8 == 0 && scrollPercentage > 0.6)
                this.fetchPhotos();
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