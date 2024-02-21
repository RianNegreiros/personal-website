'use client'

import { usePostAndComments } from '@/app/hooks/usePostAndComments'
import CommentSection from '@/app/components/CommentSection'
import Loading from '@/app/components/Loading'

import ReactMarkdown from 'react-markdown'
import rehypeRaw from 'rehype-raw'
import remarkGfm from 'remark-gfm'
import NotFound from '@/app/components/NotFound'
import ShareButtons from './ShareButtons'
import PostSuggestions from './PostSuggestions'

interface PostProps {
  params: { slug: string }
}

export default function Post({ params }: PostProps) {
  const { isLoading, post, comments, setComments } = usePostAndComments(
    params.slug,
  )

  if (isLoading) {
    return <Loading />
  }

  if (post === null) {
    return <NotFound />
  }

  return (
    <>
      <main className='bg-white pb-8 pt-4 antialiased dark:bg-gray-900 lg:pb-12 lg:pt-8'>
        <div className='mx-auto flex max-w-screen-xl justify-between px-4 '>
          <article className='format format-sm sm:format-base lg:format-lg format-blue dark:format-invert mx-auto w-full max-w-6xl'>
            <header className='pt-6 xl:pb-6'>
              <div className='space-y-1 text-center'>
                <div className='mb-3 space-y-10'>
                  <div>
                    <p className='text-base font-medium leading-6 text-dracula-pink'>
                      {new Date(post.createdAt).toLocaleDateString()}
                    </p>
                  </div>
                </div>

                <div>
                  <h1 className='md:leading-14 text-3xl font-extrabold leading-9 tracking-tight text-gray-900 dark:text-gray-100 sm:text-4xl sm:leading-10 md:text-5xl'>
                    {post.title}
                  </h1>
                </div>
              </div>
            </header>

            <div className='divide-y divide-gray-200 pb-7 dark:divide-gray-700 xl:divide-y-0'>
              <div className='prose prose-lg max-w-none pb-8 pt-10 dark:prose-invert'>
                <ReactMarkdown rehypePlugins={[remarkGfm, rehypeRaw]}>
                  {post.content}
                </ReactMarkdown>
              </div>
            </div>
          </article>
        </div>
      </main>

      <ShareButtons post={post} />

      <PostSuggestions />

      {comments.length !== 0 ? (
        <CommentSection
          slug={params.slug}
          comments={comments}
          setComments={setComments}
        />
      ) : null}
    </>
  )
}
