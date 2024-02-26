import { MetadataRoute } from 'next'
import { getPosts } from './utils/api'
import siteMetadata from './utils/siteMetaData'

interface PostData {
  slug: string
  updatedAt: string
}

export default async function sitemap(): Promise<MetadataRoute.Sitemap> {
  try {
    const response = await getPosts(0, 0)
    const data: PostData[] = response.data.items

    return data.map((post) => ({
      url: `${siteMetadata.siteUrl}/posts/${post.slug ?? null}`,
      lastModified: post.updatedAt ?? null,
      changeFrequency: 'weekly',
      priority: 0.5,
    }))
  } catch (error) {
    console.error('Failed to fetch posts:', error)
    return []
  }
}