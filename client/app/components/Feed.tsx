import { FeedItem } from '@/app/models'
import Link from 'next/link'
import Loading from './Loading'
import { useFeed } from '@/app/hooks/useFeed'
import InternalServerError from './InternalServerError'

export default function Feed() {
  const { isLoading, data, error } = useFeed()

  if (isLoading) {
    return <Loading />
  }

  if (error) {
    return <InternalServerError errorMessage={error.message} />
  }

  const options: Intl.DateTimeFormatOptions = {
    year: 'numeric',
    month: 'long',
    day: 'numeric',
    hour12: false,
  }

  return (
    <ol className='relative border-s border-gray-200 dark:border-gray-700'>
      {data.map((item: FeedItem) => (
        <li className='mb-10 ms-6' key={item.id}>
          <div className='absolute -start-1.5 mt-1.5 h-3 w-3 rounded-full border border-white bg-gray-200 dark:border-gray-900 dark:bg-gray-700'></div>
          <h3 className='mb-1 flex items-center text-lg font-semibold text-gray-900 dark:text-white'>
            {item.type === 'Post' ? (
              <Link href={`/posts/${item.slug}`}>{item.title}</Link>
            ) : (
              item.title
            )}
            <span className='me-2 ms-3 rounded bg-cyan-100 px-2.5 py-0.5 text-sm font-medium text-cyan-800 dark:bg-cyan-900 dark:text-cyan-300'>
              {item.type === 'Post' ? 'Artigo' : 'Projeto'}
            </span>
          </h3>
          <time className='mb-2 block text-sm font-normal leading-none text-gray-400 dark:text-gray-500'>
            Publicado{' '}
            {new Intl.DateTimeFormat('pt-BR', options).format(
              new Date(item.createdAt),
            )}
          </time>
          <p className='mb-4 text-base font-normal text-gray-500 dark:text-gray-400'>
            {item.summary ? item.summary : item.overview}
          </p>
          {item.url ? (
            <a
              href={item.url}
              className='light:border-gray-400 light:bg-white light:text-black light:hover:bg-gray-200 inline-flex items-center rounded-lg border px-4 py-2 text-sm font-medium transition-all duration-500 ease-in-out hover:bg-gray-300 focus:outline-none focus:ring-4 focus:ring-gray-400 dark:border-dracula-aro dark:bg-dracula-aro dark:text-white dark:hover:bg-dracula-darker'
            >
              Saiba Mais{' '}
              <svg
                className='ml-2 h-3 w-3'
                aria-hidden='true'
                xmlns='http://www.w3.org/2000/svg'
                fill='none'
                viewBox='0 0 14 10'
              >
                <path
                  stroke='currentColor'
                  strokeLinecap='round'
                  strokeLinejoin='round'
                  strokeWidth='2'
                  d='M1 5h12m0 0L9 1m4 4L9 9'
                />
              </svg>
            </a>
          ) : null}
        </li>
      ))}
    </ol>
  )
}
