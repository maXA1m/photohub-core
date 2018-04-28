/*   navbar vue instances   */
const desktopNavbar = new Vue({
    el: '#navbar',
    data: {
        nav: document.getElementById('navbar'),
        scrolling: false,
        previousTop: 0,
        currentTop: 0,
        scrollDelta: 10,
        scrollOffset: 150,
        width: window.innerWidth || document.body.clientWidth,
        isHidden: false,
        isOnTop: true
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
        checkSimpleNavigation(currentTop) {
            if (this.previousTop - currentTop > this.scrollDelta)
                this.isHidden = false;

            else if (currentTop - this.previousTop > this.scrollDelta && currentTop > this.scrollOffset)
                this.isHidden = true;

            if (currentTop < 15)
                this.isOnTop = true;
            else
                this.isOnTop = false;
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

const mobileNavbar = new Vue({
    el: '#mobileNavbar',
    data: {
        nav: document.getElementById('mobileNavbar'),
        scrolling: false,
        previousTop: 0,
        currentTop: 0,
        scrollDelta: 10,
        scrollOffset: 150,
        width: window.innerWidth || document.body.clientWidth,
        isHidden: false
    },
    created() {
        if (this.width <= 736)
            window.addEventListener('scroll', this.handleScroll);

        window.addEventListener('resize', this.onResize)
    },
    methods: {
        onResize() {
            this.width = window.innerWidth || document.body.clientWidth;

            if (this.width <= 736)
                window.addEventListener('scroll', this.handleScroll);
        },
        checkSimpleNavigation(currentTop) {
            if (this.previousTop - currentTop > this.scrollDelta)
                this.isHidden = false;

            else if (currentTop - this.previousTop > this.scrollDelta && currentTop > this.scrollOffset)
                this.isHidden = true;
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