const tagsDeatils = new Vue({
    el: '#posts',
    data: {
        currentAppUserName: document.querySelector('#body').dataset.appUser,
        appPermission: document.querySelector('#body').dataset.appPermission,
        currentTagName: document.querySelector('#posts').dataset.curTag,
        preloader: document.querySelector('#preloader'),
        message: {
            element: document.querySelector('#message'),
            text: document.querySelector('#message .message-text')
        },
        posts: [],

        canShare: navigator.share,

        page: 0,
        incallback: false,
        postsLoaded: false,

        current: null,
        commenting: false,
        modals: {
            likeActive: false,
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
                this.preloader.setAttribute('data-hidden', 'false');

                this.$http.get(`/api/photos/tags/${this.currentTagName}/${this.page}`).then(response => response.json()).then(json => {

                    if (this.posts == null)
                        this.posts = json;
                    else if (json.length == 0)
                        this.postsLoaded = true;
                    else {
                        for (let post in json)
                            this.posts.push(json[post]);
                    }

                    this.preloader.setAttribute('data-hidden', 'true');
                    this.incallback = false;
                    this.page++;
                },
                    error => {
                        this.preloader.setAttribute('data-hidden', 'true');
                        this.incallback = false;
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
        comment(post) {
            if (!this.currentAppUserName)
                return -1;

            const text = document.querySelector(`#comment${post.$id}`).value;

            if (text == '' || text.length < 1)
                return -1;

            this.commenting = true;
            this.preloader.setAttribute('data-hidden', 'false');

            const words = text.split(/[, ;.]/);;

            let ok = true;

            for (let word in words) {
                if (words[word].length > 12)
                    ok = false;
            }

            if (ok) {
                this.$http.post(`/api/comments/add?photoId=${post.$id}&text=${text}`).then(response => response.json()).then(json => {
                    post.comments.push({
                        $id: json,
                        text: text,
                        date: this.getCurrentDate(),
                        owner: {
                            userName: this.currentAppUserName
                        }
                    });

                    document.querySelector(`#comment${post.$id}`).value = '';
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
        deleteComment(comment, post) {
            if (this.currentAppUserName == comment.owner.userName || post.owner.userName == this.currentAppUserName || this.appPermission) {
                this.preloader.setAttribute('data-hidden', 'false');
                this.$http.post(`/api/comments/delete/${comment.$id}`).then(response => {
                    for (let i in post.comments) {
                        if (post.comments[i].$id == comment.$id)
                            post.comments.splice(i, 1);
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
            copyTextArea.value = `https://photohub.azurewebsites.net/photos/${id}`;
            document.body.appendChild(copyTextArea);
            copyTextArea.select();

            const successful = document.execCommand('copy');

            document.body.removeChild(copyTextArea);

            const icon = document.querySelector(`i[data-copy-id="${id}"]`);

            icon.classList.remove('far');
            icon.classList.remove('fa-clipboard');
            icon.classList.remove('fas');
            icon.classList.remove('fa-clipboard-check');

            icon.classList.add('fas');
            icon.classList.add('fa-clipboard-check');
        },
        reportPhoto(id) {
            let text = '';

            const icon = document.querySelector(`i[data-report-id="${id}"]`);

            this.$http.post(`/api/photos/report/${id}?text=${text}`).then(response => {
                icon.classList.remove('fas');
                icon.classList.remove('fa-exclamation-circle');
                icon.classList.remove('fa-check-circle');

                icon.classList.add('fas');
                icon.classList.add('fa-check-circle');
            }, response => {

            });
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

                }, response => {
                    this.message.text.innerHTML = 'Error while deleting bookmark';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

                    post.bookmarked = true;
                });
            }
        },

        sharePhoto(photo) {
            if (this.canShare) {
                navigator.share({
                    title: `${photo.owner.userName}'s photo`,
                    text: photo.description,
                    url: `https://photohub.azurewebsites.net/photos/${photo.$id}`,
                })
                    .then(() => console.log('Successful share'))
                    .catch((error) => console.log('Error sharing', error));
            }
        },

        showLikes(post) {
            this.modals.likeActive = true;
            this.current = post;
            document.documentElement.classList.add('is-clipped');
            document.body.classList.add('is-clipped');
        },
        showComments(post) {
            post.commentActive = post.commentActive ? false : true;
        },
        showMetadata(post) {
            this.modals.metadataActive = true;
            this.current = post;
            document.documentElement.classList.add('is-clipped');
            document.body.classList.add('is-clipped');
        },

        closeLikes() {
            this.modals.likeActive = false;
            document.documentElement.classList.remove('is-clipped');
            document.body.classList.remove('is-clipped');
        },
        closeMetadata() {
            this.modals.metadataActive = false;
            document.documentElement.classList.remove('is-clipped');
            document.body.classList.remove('is-clipped');
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