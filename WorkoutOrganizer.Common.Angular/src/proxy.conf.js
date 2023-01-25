const PROXY_CONFIG = [
  {
    '/api/': {
      target: "https://localhost:7279",
      secure: false,
      logLevel: "debug",
    },
   // changeOrigin: true
  }
]

module.exports = PROXY_CONFIG;
