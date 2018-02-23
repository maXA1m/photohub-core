const photosCreate = new Vue({
    el: '#photosCreate',
    data: {
        filter: 'pure',
        path: '~/images/defaults/def-photo.jpg',
        name: 'Image name',
        submited: false,
        loaded: false
    },
    created() {

    },
    mounted() {

    },
    methods: {
        filePreview(event) {
            const file = event.target.files[0];
            const imagefile = file.type;
            const match = ['image/jpeg', 'image/png', 'image/jpg', 'image/*'];

            if (!((imagefile == match[0]) || (imagefile == match[1]) || (imagefile == match[2]) || (imagefile == match[3]))) {
                //ERROR
                return false;
            }
            else {
                this.name = file.name;
                const reader = new FileReader();
                reader.onload = e => {
                    this.path = e.target.result;
                    this.loaded = true;
                };
                reader.readAsDataURL(file);
            }
        },
        pickFilter(filter) {
            this.filter = filter;
        },
        submit() {
            if(this.loaded)
                this.submited = true;
        }
    }
});