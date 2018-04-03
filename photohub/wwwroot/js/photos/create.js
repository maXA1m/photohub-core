const photosCreate = new Vue({
    el: '#photosCreate',
    data: {
        filter: 'pure',
        path: '',
        name: 'Photo name',
        pickedTags: [],
        tags: [],
        findTag: null,

        submited: false,
        loaded: false,
        metadataActive: false,
        addTagActive: false,
        message: {
            status: null,
            text: null
        },
    },
    created() {
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
        fetchTags() {
            nanobar.go(40);
            this.$http.get(`/api/tags`).then(response => response.json()).then(json => {
                this.tags = json;

                nanobar.go(100);
            },
            error => {
                nanobar.go(0);
                this.message.text = 'error while fetching tags';
                this.message.status = 'error';
            });
        },
        filePreview(event) {
            nanobar.go(40);

            const file = event.target.files[0];
            const imagefile = file.type;
            const match = ['image/jpeg', 'image/png', 'image/jpg', 'image/*'];

            if (!((imagefile == match[0]) || (imagefile == match[1]) || (imagefile == match[2]) || (imagefile == match[3]))) {
                nanobar.go(0);
                return false;
            }
            else {
                nanobar.go(70);
                this.name = file.name;
                const reader = new FileReader();
                reader.onload = e => {
                    this.path = e.target.result;
                    this.loaded = true;
                };
                nanobar.go(100);
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
            if(this.loaded)
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
        },
        closeMetadata() {
            this.metadataActive = false;
        },

        showAddTag() {
            this.addTagActive = true;
            document.getElementById('findTagInput').focus();
        },
        closeAddTag() {
            this.addTagActive = false;
        },
    }
});