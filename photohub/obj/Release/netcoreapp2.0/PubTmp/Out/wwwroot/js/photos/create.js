function makeExif() {
    const imgFile = document.getElementById('mainImg');
    let metadata;

    EXIF.getData(imgFile, function() {
        metadata = EXIF.getAllTags(this);
    });

    return {
        brand: metadata.Make,
        model: metadata.Model,
        iso: metadata.ISOSpeedRatings,
        aperture: metadata.FNumber,
        exposure: metadata.ExposureTime,
        focalLength: metadata.FocalLength
    };
}

const photosCreate = new Vue({
    el: '#photosCreate',
    data: {
        preloader: document.querySelector('#preloader'),
        message: {
            element: document.querySelector('#message'),
            text: document.querySelector('#message .message-text')
        },
        droping: false,
        filter: 'pure',
        path: '',
        name: 'Photo name',
        metadata: null,
        pickedTags: [],
        tags: [],
        findTag: null,

        submited: false,
        loaded: false,
        metadataActive: false,
        addTagActive: false
    },
    created() {
        this.fetchTags();

    },
    mounted() {
        ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
            mainCard.addEventListener(eventName, this.preventDefaults, false);
            document.body.addEventListener(eventName, this.preventDefaults, false);
        });

        ['dragenter', 'dragover'].forEach(eventName => {
            mainCard.addEventListener(eventName, this.highlight, false);
        });

        ['dragleave', 'drop'].forEach(eventName => {
            mainCard.addEventListener(eventName, this.unhighlight, false);
        });

        mainCard.addEventListener('drop', this.onDrop, false)
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
        fetchTags() {
            this.preloader.setAttribute('data-hidden', 'false');
            this.$http.get(`/api/tags`).then(response => response.json()).then(json => {
                this.tags = json;

                this.preloader.setAttribute('data-hidden', 'true');
            },
                error => {
                    this.preloader.setAttribute('data-hidden', 'true');
                    this.message.text.innerHTML = 'Error while loading tags';
                    this.message.element.setAttribute('data-message-type', 'error');
                    this.message.element.setAttribute('data-hidden', 'false');
                    setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);
                });
        },
        filePreview(event) {
            const file = event.target.files[0];

            this.uploadFile(file);
        },
        uploadFile(file) {
            if (this.loaded != true)
                this.preloader.setAttribute('data-hidden', 'false');

            const imagefile = file.type;
            const match = ['image/jpeg', 'image/png', 'image/jpg', 'image/*'];

            if (!((imagefile == match[0]) || (imagefile == match[1]) || (imagefile == match[2]) || (imagefile == match[3]))) {

                this.message.text.innerHTML = 'Error while uploading photo';
                this.message.element.setAttribute('data-message-type', 'error');
                this.message.element.setAttribute('data-hidden', 'false');
                setTimeout(() => { this.message.element.setAttribute('data-hidden', 'true'); }, 5000);
                this.preloader.setAttribute('data-hidden', 'true');
            }
            else {
                this.name = file.name;

                const reader = new FileReader();
                reader.onload = e => {
                    this.path = e.target.result;
                    this.loaded = true;
                    this.preloader.setAttribute('data-hidden', 'true');
                    
                    setTimeout(() => { this.metadata = makeExif(); }, 1000);
                };
                reader.readAsDataURL(file);
            }
        },
        pickFilter(filter) {
            this.filter = filter;
        },
        clickFile() {
            document.getElementById('file').click();
        },
        submit() {
            if (!this.loaded)
                return -1;

            this.submited = true;
            document.getElementById("photosCreate").submit(); 
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
            this.addTagActive = true;
            document.getElementById('findTagInput').focus();
            document.documentElement.classList.add('is-clipped');
            document.body.classList.add('is-clipped');
        },
        closeAddTag() {
            this.addTagActive = false;
            document.documentElement.classList.remove('is-clipped');
            document.body.classList.remove('is-clipped');
        },

        onDrop(e) {
            let dt = e.dataTransfer;
            let file = dt.files[0];
            if(!this.loaded && !this.submited)
                this.uploadFile(file);
        },
        highlight(e) {
            if (!this.loaded && !this.submited)
                this.droping = true;
        },
        unhighlight(e) {
            this.droping = false;
        },
        preventDefaults(e) {
            e.preventDefault();
            e.stopPropagation();
        }
    }
});