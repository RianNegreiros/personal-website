'use client'

import Link from 'next/link'
import Loading from '../components/Loading'
import { usePosts } from '../hooks/usePosts'
import InternalServerError from '../components/InternalServerError'
import Pagination from '../components/PaginationButtons'

export default function PostsPage() {
  const { isLoading, data, pageNumber, nextPage, handlePageChange, error } =
    usePosts(1, 10)

  if (isLoading) {
    return <Loading />
  }

  if (error) {
    return <InternalServerError errorMessage={error.message} />
  }

  return (
    <div className='mx-auto flex min-h-screen max-w-6xl flex-col divide-y divide-gray-200 px-4 dark:divide-gray-700 sm:px-6 lg:px-8'>
      <div className='space-y-2 pb-8 pt-6 md:space-y-5'></div>
      <ul>
        {data.map((post) => (
          <li key={post.id} className='py-4'>
            <article className='space-y-2 xl:grid xl:grid-cols-4 xl:items-baseline xl:space-y-0'>
              <div>
                <p className='text-base font-medium leading-6 text-dracula-dracula-500'>
                  {new Date(post.createdAt).toLocaleDateString()}
                </p>
              </div>

              <Link
                href={`/posts/${post.slug}`}
                prefetch
                className='space-y-3 xl:col-span-3'
              >
                <div>
                  <h3 className='text-2xl font-bold leading-8 tracking-tight text-gray-900 dark:text-gray-100'>
                    {post.title}
                  </h3>
                </div>

                <p className='prose line-clamp-2 max-w-none text-gray-500 dark:text-gray-400'>
                  {post.summary}
                </p>
              </Link>
            </article>
          </li>
        ))}
        <Pagination
          pageNumber={pageNumber}
          nextPage={nextPage}
          handlePageChange={handlePageChange}
        />
      </ul>
    </div>
  )
}
