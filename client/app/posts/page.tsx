"use client"

import Link from "next/link";
import { Post } from "../models";
import { getPosts } from "../utils/api";
import { useEffect, useState } from "react";
import Loading from "../components/Loading";

async function getData(pageNumber: number, pageSize: number) {
  return await getPosts(pageNumber, pageSize);
}

export default function BlogPage() {
  const [isLoading, setIsLoading] = useState(true);
  const [data, setData] = useState<Post[]>([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalCount, setTotalCount] = useState(0);
  const [nextPage, setNextPage] = useState(false);

  useEffect(() => {
    async function fetchData() {
      const fetchedData = await getData(pageNumber, pageSize);
      setData(fetchedData.data.items);
      setNextPage(fetchedData.data.hasNextPage);
      setTotalCount(fetchedData.data.totalCount);
      setIsLoading(false);
    }
    fetchData();
  }, [pageNumber, pageSize]);

  const handlePageChange = async (pageNumber: number) => {
    setIsLoading(true);
    const fetchedData = await getData(pageNumber, pageSize);
    setData(fetchedData.data.items);
    setPageNumber(fetchedData.data.currentPage);
    setNextPage(fetchedData.data.hasNextPage);
    setIsLoading(false);
  }

  return (
    <>
      {isLoading ? (
        <Loading />
      ) : (
        <div className="divide-y divide-gray-200 dark:divide-gray-700">
          <div className="space-y-2 pt-6 pb-8 md:space-y-5"></div>

          <ul>
            {data.map((post) => (
              <li key={post.id} className="py-4">
                <article className="space-y-2 xl:grid xl:grid-cols-4 xl:items-baseline xl:space-y-0">
                  <div>
                    <p className="text-base font-medium leading-6 text-teal-500">
                      {new Date(post.createdAt).toLocaleDateString()}
                    </p>
                  </div>

                  <Link href={`/posts/${post.slug}`}
                    prefetch
                    className="space-y-3 xl:col-span-3"
                  >
                    <div>
                      <h3 className="text-2xl font-bold leading-8 tracking-tight text-gray-900 dark:text-gray-100">
                        {post.title}
                      </h3>
                    </div>

                    <p className="prose max-w-none text-gray-500 dark:text-gray-400 line-clamp-2">
                      {post.summary}
                    </p>
                  </Link>
                </article>
              </li>
            ))}
            <div className="flex flex-col items-center">
              <div className="inline-flex mt-2 xs:mt-0">
                {pageNumber > 1 && (
                  <button onClick={() => handlePageChange(pageNumber - 1)} className={`flex items-center justify-center px-4 h-10 text-base font-medium text-white bg-gray-800 rounded-l hover:bg-gray-900 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white ${nextPage ? "rounded-l " : "rounded"}`}>
                    Anterior
                  </button>
                )}
                {nextPage && (
                  <button onClick={() => handlePageChange(pageNumber + 1)} className={`flex items-center justify-center px-4 h-10 text-base font-medium text-white bg-gray-800 border-0 border-gray-700 hover:bg-gray-900 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white ${pageNumber > 1 ? "border-l rounded-r" : "border rounded"}`}>
                    Próximo
                  </button>
                )}
              </div>
            </div>
          </ul>
        </div>
      )}
    </>
  )
}