var gulp = require("gulp"),
    fs = require("fs"),
    sass = require("gulp-sass");

gulp.task("site-sass", function () {
    return gulp.src('Styles/site.scss')
        .pipe(sass())
        .pipe(gulp.dest('wwwroot/css'));
});