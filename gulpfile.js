var gulp = require("gulp"),
    fs = require("fs"),
    sass = require("gulp-sass");

gulp.task("site-sass", function () {
    return gulp.src('Styles/site.scss')
        .pipe(sass())
        .pipe(gulp.dest('wwwroot/css'));
});

gulp.task("site-bootstrap", function () {
    return gulp.src('Styles/Bootstrap/scss/bootstrap.scss')
        .pipe(sass())
        .pipe(gulp.dest('wwwroot/lib/bootstrap/dist/css'));
});