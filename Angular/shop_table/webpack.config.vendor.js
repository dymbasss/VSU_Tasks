plugins: [
    new webpack.ProvidePlugin({ $: 'jquery', jQuery: 'jquery', Popper: 'popper.js' }), // Maps these identifiers to the jQuery package (because Bootstrap expects it to be a global variable)
    new webpack.ContextReplacementPlugin(/\@angular\b.*\b(bundles|linker)/, path.join(__dirname, './ClientApp')), // Workaround for https://github.com/angular/angular/issues/11580
    new webpack.ContextReplacementPlugin(/angular(\\|\/)core(\\|\/)@angular/, path.join(__dirname, './ClientApp')), // Workaround for https://github.com/angular/angular/issues/14898
    new webpack.IgnorePlugin(/^vertx$/) // Workaround for https://github.com/stefanpenner/es6-promise/issues/100
] 