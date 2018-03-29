const photosEdit = new Vue({
    el: '#photosEdit',
    data: {
        filter: document.querySelector('#container-filters').dataset.currentFilterName,
        submited: false,
        metadataActive: false,
    },
    created() {

    },
    mounted() {

    },
    methods: {
        pickFilter: function (f) {
            this.filter = f;
        },
        submit() {
            this.submited = true;
        },

        showMetadata() {
            this.metadataActive = true;
        },
        closeMetadata() {
            this.metadataActive = false;
        },
    }
});