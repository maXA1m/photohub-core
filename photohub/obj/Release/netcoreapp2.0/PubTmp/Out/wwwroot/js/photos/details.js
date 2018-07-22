const photosDetails = new Vue({
    el: '#photosDetails',
    data: {
        currentAppUserName: document.querySelector('#body').dataset.appUser,
        appPermission: document.querySelector('#body').dataset.appPermission,
        preloader: document.querySelector('#preloader'),
        id: document.querySelector('#photosDetails').dataset.postId,
        message: {
            element: document.querySelector('#message'),
            text: document.querySelector('#message .message-text')
        },

        post: null,
        recommendations: null,
        recommendedTagName: null,

        canShare: navigator.share,

        commenting: false,
        modals: {
            likeActive: false,
            metadataActive: false
        }
    },
    created() {
        this.fetchPhoto();
    },
    mounted() {

    },
    methods: {
        fetchPhoto() {
            this.preloader.setAttribute('data-hidden', 'false');

            this.$http.get(`/api/photos/details/${this.id}`).then(response => response.json()).then(json => {
                this.post = json;

                this.fetchPhotosForTag();

                this.preloader.setAttribute('data-hidden', 'true');
            },
            error => {
                this.preloader.setAttribute('data-hidden', 'true');
                this.message.text.innerHTML = 'Error while loading photos';
                this.message.element.setAttribute('data-message-type', 'error');
                this.message.element.setAttribute('data-hidden', 'false');
                setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);
            });
        },
        fetchPhotosForTag() {
            if (!this.post.tags || !this.post.tags.length)
                return -1;

            this.preloader.setAttribute('data-hidden', 'false');

            this.recommendedTagName = this.post.tags[0].name;

            if ((this.post.tags.length - 1) > 0) {
                const index = Math.floor(Math.random() * this.post.tags.length);
                this.recommendedTagName = this.post.tags[index].name;
            }

            this.$http.get(`/api/photos/tag/${this.recommendedTagName}`).then(response => response.json()).then(json => {
                this.recommendations = json;

                for (let i in this.recommendations) {
                    if (this.recommendations[i].$id == this.post.$id)
                        this.recommendations.splice(i, 1);
                }

                this.preloader.setAttribute('data-hidden', 'true');
            },
            error => {
                this.preloader.setAttribute('data-hidden', 'true');
                this.message.text.innerHTML = 'Error while loading recommendations';
                this.message.element.setAttribute('data-message-type', 'error');
                this.message.element.setAttribute('data-hidden', 'false');
                setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);
            });
        },
        like() {
            if (!this.currentAppUserName)
                return -1;

            if (!this.post.liked) {
                this.post.liked = true;
                this.post.likes.push({
                    date: this.getCurrentDate(),
                    owner: {
                        userName: this.currentAppUserName
                    }
                });
                this.$http.post(`/api/likes/add/${this.id}`).then(response => {

                }, response => {
                    this.message.text.innerHTML = 'Error while liking photo';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

                    this.post.liked = false;
                    for (let i in this.post.likes) {
                        if (this.post.likes[i].owner.userName == this.currentAppUserName)
                            this.post.likes.splice(i, 1);
                    }
                });
            }
        },
        dislike() {
            if (!this.currentAppUserName)
                return -1;

            if (this.post.liked) {
                this.post.liked = false;
                for (let i in this.post.likes) {
                    if (this.post.likes[i].owner.userName == this.currentAppUserName)
                        this.post.likes.splice(i, 1);
                }
                this.$http.post(`/api/likes/delete/${this.id}`).then(response => {

                }, response => {
                    this.message.text.innerHTML = 'Error while disliking photo';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

                    this.post.liked = true;
                    this.post.likes.push({
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

            const words = text.split(/[, ;.]/);;

            let ok = true;

            for (let word in words) {
                if (words[word].length > 12)
                    ok = false;
            }

            if (ok) {
                this.$http.post(`/api/comments/add?photoId=${this.id}&text=${text}`).then(response => response.json()).then(json => {
                    this.post.comments.push({
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
            if (this.currentAppUserName == comment.owner.userName || this.current.owner.userName == this.currentAppUserName || this.appPermission) {
                this.preloader.setAttribute('data-hidden', 'false');
                this.$http.post(`/api/comments/delete/${comment.$id}`).then(response => {
                    for (let i in this.post.comments) {
                        if (this.post.comments[i].$id == comment.$id)
                            this.post.comments.splice(i, 1);
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

        bookmark() {
            if (!this.currentAppUserName)
                return -1;

            if (!this.post.bookmarked) {
                this.post.bookmarked = true;

                this.$http.post(`/api/photos/bookmark/${this.post.$id}`).then(response => {

                }, response => {
                    this.message.text.innerHTML = 'Error while bookmarking';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

                    this.post.bookmarked = false;
                });
            }
        },
        disbookmark() {
            if (!this.currentAppUserName)
                return -1;

            if (this.post.bookmarked) {
                this.post.bookmarked = false;

                this.$http.post(`/api/photos/dismiss/bookmark/${this.post.$id}`).then(response => {

                }, response => {
                    this.message.text.innerHTML = 'Error while deleting bookmark';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);

                    this.post.bookmarked = true;
                });
            }
        },

        copyToClipboard() {
            const copyTextArea = document.createElement('textarea');
            copyTextArea.value = `https://photohub.azurewebsites.net/photos/${this.post.$id}`;
            document.body.appendChild(copyTextArea);
            copyTextArea.select();

            const successful = document.execCommand('copy');

            document.body.removeChild(copyTextArea);

            const icon = document.querySelector(`i[data-copy-id="${this.post.$id}"]`);

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
        sharePhoto() {
            if (this.canShare) {
                navigator.share({
                    title: `${this.post.owner.userName}'s photo`,
                    text: this.post.description,
                    url: `https://photohub.azurewebsites.net/photos/${this.post.$id}`,
                })
                    .then(() => console.log('Successful share'))
                    .catch((error) => console.log('Error sharing', error));
            }
        },

        showMetadata() {
            this.modals.metadataActive = true;
            document.documentElement.classList.add('is-clipped');
            document.body.classList.add('is-clipped');
        },
        closeMetadata() {
            this.modals.metadataActive = false;
            document.documentElement.classList.remove('is-clipped');
            document.body.classList.remove('is-clipped');
        },

        showLikes() {
            this.modals.likeActive = true;
            document.documentElement.classList.add('is-clipped');
            document.body.classList.add('is-clipped');
        },
        closeLikes() {
            this.modals.likeActive = false;
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