const photosCreate = new Vue({
    el: '#photosCreate',
    data: {
        filter: 'pure',
        path: '',
        name: 'Your photo',
        submited: false,
        loaded: false
    },
    created() {

    },
    mounted() {

    },
    methods: {
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
        }
    }
});