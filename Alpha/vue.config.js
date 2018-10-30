const path = require("path");

// vue.config.js
module.exports = {
  outputDir: "wwwroot",
  devServer: {
    proxy: "http://localhost:5000"
  },
  chainWebpack: config => {
    const types = ["vue-modules", "vue", "normal-modules", "normal"];
    types.forEach(type =>
      addStyleResource(config.module.rule("scss").oneOf(type))
    );
  }
};

function addStyleResource(rule) {
  rule
    .use("style-resource")
    .loader("style-resources-loader")
    .options({
      patterns: [
        path.resolve(__dirname, './src/styles/vendors/_family.scss'),
        path.resolve(__dirname, './src/styles/functions/*.scss'),
        path.resolve(__dirname, './src/styles/mixins/*.scss'),
        path.resolve(__dirname, './src/styles/variables/*.scss'),
      ]
    });
}
