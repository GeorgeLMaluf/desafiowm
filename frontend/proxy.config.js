const proxy = [
    {
        context: '/api',
        target: 'http://localhost:50685/api',
        pathRewrite: { '^/api': ''}
    }
]
module.exports = proxy