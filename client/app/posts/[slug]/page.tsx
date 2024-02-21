import Post from '@/app/components/Post'
import { getPostBySlug } from '@/app/utils/api'
import siteMetadata from '@/app/utils/siteMetaData'
import { Metadata } from 'next'
import { cache } from 'react'

const getPosts = cache(async (slug: string) => {
  const postData = await getPostBySlug(slug)
  return postData
})

export async function generateMetadata({
  params,
}: {
  params: { slug: string }
}): Promise<Metadata> {
  const postData = await getPosts(params.slug)

  return {
    metadataBase: new URL(siteMetadata.siteUrl),
    title: `${postData.data.title} | ${siteMetadata.title}`,
    description:
      postData.data.summary.length > 155
        ? postData.data.summary.substring(0, 155) + '...'
        : postData.data.summary,
    keywords: [
      ...postData.data.title.split(' '),
      ...postData.data.summary.split(' ').slice(0, 5),
    ],
    openGraph: {
      title: postData.data.title,
      description: postData.data.summary,
      url: `${siteMetadata.siteUrl}/posts/${postData.data.slug}`,
      siteName: siteMetadata.title,
      locale: 'pt_BR',
      type: 'article',
      publishedTime: new Date(postData.data.createdAt).toISOString(),
      modifiedTime: new Date(postData.data.updatedAt).toISOString(),
    },
    robots: {
      index: true,
      follow: true,
    },
  }
}

export default async function PostPage({
  params,
}: {
  params: { slug: string }
}) {
  return <Post params={params} />
}
