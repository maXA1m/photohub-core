/*   navbar vue instance   */
const navbar = new Vue({
    el: '#navbar',
    data: {
        nav: document.getElementById('navbar'),
        scrolling: false,
        previousTop: 0,
        currentTop: 0,
        scrollDelta: 10,
        scrollOffset: 150,
        width: window.innerWidth || document.body.clientWidth,
        isHidden: false
    },
    created() {
        if(this.width > 736)
            window.addEventListener('scroll', this.handleScroll);

        window.addEventListener('resize', this.onResize)
    },
    methods: {
        onResize() {
            this.width = window.innerWidth || document.body.clientWidth;

            if (this.width > 736)
                window.addEventListener('scroll', this.handleScroll);
        },
        //toTopPage() {
        //    document.documentElement.scrollTop = 0;
        //},
        checkSimpleNavigation(currentTop) {
            if (this.previousTop - currentTop > this.scrollDelta)
                this.isHidden = false;

            else if (currentTop - this.previousTop > this.scrollDelta && currentTop > this.scrollOffset) {
                this.isHidden = true;
                this.navActive = false;
            }
        },
        autoHideHeader() {
            const currentTop = document.documentElement.scrollTop;

            this.checkSimpleNavigation(currentTop);

            this.previousTop = currentTop;
            this.scrolling = false;
        },
        handleScroll() {
            if (!this.scrolling) {
                this.scrolling = true;
                (!window.requestAnimationFrame)
                    ? setTimeout(this.autoHideHeader, 250)
                    : requestAnimationFrame(this.autoHideHeader);
            }
        }
    }
});