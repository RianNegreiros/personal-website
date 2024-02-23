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
import NewsLetter from './NewsLetter'

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

  const options: Intl.DateTimeFormatOptions = {
    year: 'numeric',
    month: 'long',
    day: 'numeric',
    hour: 'numeric',
    minute: 'numeric',
    hour12: false,
  }

  const date = new Date(post.createdAt)

  return (
    <main className='bg-white pb-8 pt-4 antialiased dark:bg-gray-900 lg:pb-12 lg:pt-8'>
      <div className='mx-auto flex max-w-screen-xl justify-between px-4'>
        <article className='format format-sm sm:format-base lg:format-lg format-cyan dark:format-invert mx-auto w-full max-w-6xl'>
          <header className='pt-6 xl:pb-6'>
            <div className='space-y-1 text-center'>
              <div className='mb-3 space-y-10'></div>
              <h1 className='md:leading-14 text-3xl font-extrabold leading-9 tracking-tight text-gray-900 dark:text-gray-100 sm:text-4xl sm:leading-10 md:text-5xl'>
                {post.title}
              </h1>
            </div>

            <div className='inline-flex w-full items-center justify-center space-x-5 pt-6'>
              <p className='text-base font-medium leading-6 text-dracula-purple'>
                {new Intl.DateTimeFormat('pt-BR', options).format(date)}
              </p>
              <ShareButtons post={post} />
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

      {comments.length !== 0 ? (
        <CommentSection
          slug={params.slug}
          comments={comments}
          setComments={setComments}
        />
      ) : null}

      <PostSuggestions />

      <NewsLetter />
    </main>
  )
}
