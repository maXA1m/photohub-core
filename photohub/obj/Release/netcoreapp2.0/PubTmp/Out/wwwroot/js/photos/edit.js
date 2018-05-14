const photosEdit = new Vue({
    el: '#photosEdit',
    data: {
        filter: document.querySelector('#container-filters').dataset.currentFilterName,
        preloader: document.querySelector('#preloader'),
        currentPhotoId: document.querySelector('#photosEdit').dataset.currentPhotoId,
        message: {
            element: document.querySelector('#message'),
            text: document.querySelector('#message .message-text')
        },
        tags: [],
        post: null,
        pickedTags: [],
        tags: [],
        findTag: null,

        submited: false,
        metadataActive: false,
        addTagActive: false
    },
    created() {
        this.fetchPhoto();
        this.fetchTags();
    },
    mounted() {

    },
    computed: {
        filteredTags() {
            if (this.findTag) {
                return this.tags.filter(t => {
                    return t.name.toLowerCase().includes(this.findTag.toLowerCase())
                });
            }

            return this.tags;
        }
    },
    methods: {
        fetchPhoto() {
            this.preloader.setAttribute('data-hidden', 'false');
            this.$http.get(`/api/photos/details/${this.currentPhotoId}`).then(response => response.json()).then(json => {
                this.post = json;

                for (let i in json.tags)
                    this.pickedTags.push(json.tags[i].name);

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
        fetchTags() {
            this.$http.get(`/api/tags`).then(response => response.json()).then(json => {
                this.tags = json;
            },
            error => {
                this.message.text.innerHTML = 'Error while loading tags';
                this.message.element.setAttribute('data-message-type', 'error');
                this.message.element.setAttribute('data-hidden', 'false');
                setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);
            });
        },
        pickFilter(f) {
            this.filter = f;
        },
        submit() {
            this.submited = true;
        },

        findTagEnter(event) {
            event.preventDefault();

            let tags;

            if (this.findTag) {
                tags = this.tags.filter(t => {
                    return t.name.toLowerCase().includes(this.findTag.toLowerCase())
                });
            }
            else {
                tags = this.tags;
            }

            this.addTag(tags[0].name);
            this.findTag = '';
            this.closeAddTag();
        },

        addTag(tagName) {
            if (!this.pickedTags.join().includes(tagName))
                this.pickedTags.push(tagName);

            this.closeAddTag();
        },
        removeTag(tagName) {
            for (let i in this.pickedTags) {
                if (this.pickedTags[i] == tagName)
                    this.pickedTags.splice(i, 1);
            }
        },

        showMetadata() {
            this.metadataActive = true;
            document.documentElement.classList.add('is-clipped');
            document.body.classList.add('is-clipped');
        },
        closeMetadata() {
            this.metadataActive = false;
            document.documentElement.classList.remove('is-clipped');
            document.body.classList.remove('is-clipped');
        },

        showAddTag() {
            this.addTagActive = true
            document.getElementById('findTagInput').focus();
            document.documentElement.classList.add('is-clipped');
            document.body.classList.add('is-clipped');
        },
        closeAddTag() {
            this.addTagActive = false;
            document.documentElement.classList.remove('is-clipped');
            document.body.classList.remove('is-clipped');
        },
    }
});