export default {
    content: [
        "./maximApp.SharedUI/**/*.razor",
        "./maximApp.SharedUI/**/*.html",
        "./maximApp.SharedUI/**/*.cshtml",

        "./maximApp.WebHost/**/*.razor",
        "./maximApp.WebHost/**/*.cshtml",
    ],
    theme: {
        extend: {
            data: {
                active: 'active="true"', // 讓 data-active 變為可選用的狀態
            },
        },
    }
};
