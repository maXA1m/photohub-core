const uploadAvatar = new Vue({
    el: '#uploadAvatar',
    data: {
        preloader: document.querySelector('#preloader'),
        message: {
            element: document.querySelector('#message'),
            text: document.querySelector('#message .message-text')
        },
        path: '',
        name: 'Photo name',

        loaded: false
    },
    created() {

    },
    mounted() {

    },
    computed: {

    },
    methods: {
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
                };
                reader.readAsDataURL(file);
            }
        }
    }
});