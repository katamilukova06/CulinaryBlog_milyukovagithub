document.addEventListener('DOMContentLoaded', function () {
    window.addEventListener('scroll', function () {
        let header = document.getElementById("header-top");
        let scrollTop = window.scrollY;
        let maxScroll = 250;

        let opacity = Math.min(scrollTop / maxScroll, 1);
        header.style.backgroundColor = `rgba(250, 240, 230, ${opacity})`;
    });
    function toggleMenu() {
        const sideMenu = document.getElementById('side-menu');

        sideMenu.classList.toggle('active');
    }
    document.getElementById('hamburger').addEventListener('click', toggleMenu);
});
