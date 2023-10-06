import { useState, useEffect } from 'react';
import { Post, Project } from '../models';
import { getFeed, getProjects } from '../utils/api';

import Link from 'next/link';
import Loading from './Loading';

export default function Feed() {
  const [isLoading, setIsLoading] = useState(true);
  const [data, setData] = useState<Post[] | Project[]>([]);

  useEffect(() => {
    async function fetchData() {
      const posts = await getFeed();
      const projects = await getProjects();
      const mergedData = [...posts.data, ...projects.data];
      mergedData.sort((a: Post | Project, b: Post | Project) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime());
      setData(mergedData);
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
            {data.map((item: Post | Project) => (
              <li className="mb-10 ml-4" key={item.id}>
                <div className="absolute w-3 h-3 bg-gray-200 rounded-full mt-1.5 -left-1.5 border border-white dark:border-gray-900 dark:bg-gray-700"></div>
                <time className="mb-1 text-sm font-normal leading-none text-gray-400 dark:text-gray-500">{new Date(item.createdAt).toLocaleDateString()}</time>
                {'slug' in item ?
                  <h3 className="text-lg font-semibold text-gray-900 dark:text-white">{<Link href={`/post/${item.slug}`}>{item.title}</Link>}</h3>
                  :
                  <h3 className="text-lg font-semibold text-gray-900 dark:text-white">{item.title}</h3>}
                <p className="mb-4 text-base font-normal text-gray-500 dark:text-gray-400">
                  {'summary' in item ? item.summary : item.overview}
                </p>
                {'url' in item ?
                  <a href={item.url} className="inline-flex items-center px-4 py-2 text-sm font-medium text-gray-900 bg-white border border-gray-200 rounded-lg hover:bg-gray-100 hover:text-blue-700 focus:z-10 focus:ring-4 focus:outline-none focus:ring-gray-200 focus:text-blue-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700 dark:focus:ring-gray-700">Saiba Mais <svg className="w-3 h-3 ml-2" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 10">
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
