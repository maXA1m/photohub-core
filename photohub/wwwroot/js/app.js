/*     nanobar init     */
const nanobar = new Nanobar({
    id: 'nanobar'
});

/*   navbar vue instance   */
const navbar = new Vue({
    el: '#navbar',
    data: {
        navActive: false,
        nav: document.getElementById('navbar'),
        scrolling: false,
        previousTop: 0,
        currentTop: 0,
        scrollDelta: 10,
        scrollOffset: 150,
        isHidden: false
    },
    created() {
        window.addEventListener('scroll', this.handleScroll);
    },
    methods: {
        clickNavbar() {
            this.navActive = this.navActive ? false : true;
        },
        toTopPage() {
            document.documentElement.scrollTop = 0;
        },
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