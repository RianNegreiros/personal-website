/** @type {import('next-sitemap').IConfig} */
module.exports = {
  siteUrl: process.env.SITE_URL || 'https://riannegreiros.dev',
  generateRobotsTxt: true,
  exclude: ['/signin', '/signup'],
  robotsTxtOptions: {
    policies: [
      {
        userAgent: '*',
        allow: '/',
        disallow: ['/signin', '/signup', '/terms'],
      },
    ],
  },
}