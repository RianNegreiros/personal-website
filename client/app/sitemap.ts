import { MetadataRoute } from 'next'
import { getPosts } from './utils/api'
import siteMetadata from './utils/siteMetaData'

interface PostData {
  slug: string
  updatedAt: string
}

export default async function sitemap(): Promise<MetadataRoute.Sitemap> {
  const response = await getPosts(0, 0)
  const data: PostData[] = await response.data.items

  return data.map((article) => ({
    url: `${siteMetadata.siteUrl}/posts/${article.slug}`,
    lastModified: article.updatedAt,
    changeFrequency: 'weekly',
    priority: 0.5,
  }))
}
