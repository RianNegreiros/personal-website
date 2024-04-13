import React, { useEffect, useState } from 'react'
import { Post } from '@/app/models'
import { getPostsSuggestions } from '../utils/api'

interface PostSuggestionsProps {
  slug: string
}

export default function PostSuggestions({ slug }: PostSuggestionsProps) {
  const [posts, setPosts] = useState<Post[]>([])

  useEffect(() => {
    getPostsSuggestions(4, slug)
      .then((data) => setPosts(data.data))
      .catch((error) => console.error(error))
  }, [slug])

  return (
    <aside
      aria-label='Sugestão de artigos'
      className='bg-gray-50 py-4 dark:bg-gray-800'
    >
      <div className='mx-auto max-w-screen-xl px-4'>
        <h2 className='mb-8 text-2xl font-bold text-gray-900 dark:text-white'>
          Sugestão de artigos
        </h2>
        <div className='grid gap-12 sm:grid-cols-2 lg:grid-cols-4'>
          {posts.map((post, index) => (
            <article key={index} className='max-w-xs'>
              <h2 className='mb-2 text-xl font-bold leading-tight text-gray-900 dark:text-white'>
                <a href={`/posts/${post.slug}`}>{post.title}</a>
              </h2>
              <p className='mb-4 text-gray-500 dark:text-gray-400'>
                {post.summary}
              </p>
            </article>
          ))}
        </div>
      </div>
    </aside>
  )
}
