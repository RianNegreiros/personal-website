import { useState, useEffect } from 'react';
import { FeedItem } from '../models';
import { getFeed } from '../utils/api';
import Link from 'next/link';
import Loading from './Loading';

export default function Feed() {
  const [isLoading, setIsLoading] = useState(true);
  const [data, setData] = useState<FeedItem[]>([]);

  useEffect(() => {
    async function fetchData() {
      const feed = await getFeed();
      setData(feed.data);
      setIsLoading(false);
    }
    fetchData();
  }, []);

  return (
    <>
      {isLoading ? (
        <Loading />
      ) : (
        <ol className="relative border-l border-gray-200 dark:border-gray-700">
          {data.map((item: FeedItem) => (
            <li className="mb-10 mt-10 ml-10" key={item.id}>
              <span className="absolute flex items-center justify-center w-6 h-6 bg-blue-100 rounded-full -left-3 ring-8 ring-white dark:ring-gray-900 dark:bg-blue-900">
                {item.summary ? (
                  <svg className="w-2.5 h-2.5 text-dracula-pink-800 dark:text-dracula-pink-300" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 20">
                    <path d="M19 4h-1a1 1 0 1 0 0 2v11a1 1 0 0 1-2 0V2a2 2 0 0 0-2-2H2a2 2 0 0 0-2 2v15a3 3 0 0 0 3 3h14a3 3 0 0 0 3-3V5a1 1 0 0 0-1-1ZM3 4a1 1 0 0 1 1-1h3a1 1 0 0 1 1 1v3a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1V4Zm9 13H4a1 1 0 0 1 0-2h8a1 1 0 0 1 0 2Zm0-3H4a1 1 0 0 1 0-2h8a1 1 0 0 1 0 2Zm0-3H4a1 1 0 0 1 0-2h8a1 1 0 1 1 0 2Zm0-3h-2a1 1 0 0 1 0-2h2a1 1 0 1 1 0 2Zm0-3h-2a1 1 0 0 1 0-2h2a1 1 0 1 1 0 2Z" />
                    <path d="M6 5H5v1h1V5Z" />
                  </svg>
                ) : (
                  <svg className="w-2.5 h-2.5 text-dracula-pink-800 dark:text-dracula-pink-300" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 20">
                    <path fillRule="evenodd" d="M10 .333A9.911 9.911 0 0 0 6.866 19.65c.5.092.678-.215.678-.477 0-.237-.01-1.017-.014-1.845-2.757.6-3.338-1.169-3.338-1.169a2.627 2.627 0 0 0-1.1-1.451c-.9-.615.07-.6.07-.6a2.084 2.084 0 0 1 1.518 1.021 2.11 2.11 0 0 0 2.884.823c.044-.503.268-.973.63-1.325-2.2-.25-4.516-1.1-4.516-4.9A3.832 3.832 0 0 1 4.7 7.068a3.56 3.56 0 0 1 .095-2.623s.832-.266 2.726 1.016a9.409 9.409 0 0 1 4.962 0c1.89-1.282 2.717-1.016 2.717-1.016.366.83.402 1.768.1 2.623a3.827 3.827 0 0 1 1.02 2.659c0 3.807-2.319 4.644-4.525 4.889a2.366 2.366 0 0 1 .673 1.834c0 1.326-.012 2.394-.012 2.72 0 .263.18.572.681.475A9.911 9.911 0 0 0 10 .333Z" clipRule="evenodd" />
                  </svg>
                )}
              </span>
              <time className="mb-1 text-sm font-normal leading-none text-gray-400 dark:text-gray-500">{new Date(item.createdAt).toLocaleDateString()}</time>
              {item.slug ?
                <h3 className="text-lg font-semibold text-gray-900 dark:text-white">{<Link href={`/posts/${item.slug}`}>{item.title}</Link>}</h3>
                :
                <h3 className="text-lg font-semibold text-gray-900 dark:text-white">{item.title}</h3>}
              <p className="mb-4 text-base font-normal text-gray-500 dark:text-gray-400">
                {item.summary ? item.summary : item.overview}
              </p>
              {item.url ?
                <a href={item.url} className="inline-flex items-center px-4 py-2 text-sm font-medium text-gray-900 bg-white border border-gray-200 rounded-lg hover:bg-gray-100 hover:text-dracula-pink focus:z-10 focus:ring-4 focus:outline-none focus:ring-gray-200 focus:text-dracula-pink dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:text-dracula-pink dark:hover:bg-gray-700 dark:focus:ring-gray-700 transition duration-500">Saiba Mais <svg className="w-3 h-3 ml-2" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 10">
                  <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M1 5h12m0 0L9 1m4 4L9 9" />
                </svg></a>
                :
                null}
            </li>
          ))}
        </ol>
      )}
    </>
  );
}
