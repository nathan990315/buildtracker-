/// <binding BeforeBuild='sass-compile, typescript' ProjectOpened='watch-sass' />
var gulp = require("gulp");
var sass = require("gulp-sass");
var cleanCss = require("gulp-clean-css");
var sourceMaps = require("gulp-sourcemaps");
var ts = require("gulp-typescript");
var uglify = require("gulp-uglify-es").default;
var autoprefixer = require("gulp-autoprefixer");
var pipe = require("multipipe");

function catchError(err)
{
    if (typeof (err) !== "undefined" && typeof (err.hasOwnProperty("messageFormatted")) !== "undefined")
    {
        console.log(err.messageFormatted);
    }
    else
    {
        console.log("Error in processing task...");
    }
}

gulp.task("sass-compile",
    function()
    {
        var pipes = pipe(sourceMaps.init(),
            sass(),
            autoprefixer({
                browsers: ["> 1%", "IE 10-11", "last 5 versions"],
                cascade: false
            }),
            cleanCss(),
            sourceMaps.write("./"),
            gulp.dest("./wwwroot/res/css/"));

        return gulp.src("./res/scss/*.scss")
            .pipe(pipes);
    });


var tsProject = ts.createProject("tsconfig.json");
gulp.task("typescript",
    function()
    {
        return gulp.src("./res/ts/*.ts")
            .pipe(sourceMaps.init())
            .pipe(tsProject())
            .js
            .pipe(uglify())
            .pipe(sourceMaps.write("./"))
            .pipe(gulp.dest("./wwwroot/res/js/"));
    });

gulp.task("watch-sass",
    function()
    {
        gulp.watch("./res/scss/**.scss", gulp.series("sass-compile"));
        gulp.watch("./res/ts/*.ts", gulp.series("typescript"));
    });