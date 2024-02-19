"use client"

import Link from "next/link";
import { Post } from "../models";
import { getPosts } from "../utils/api";
import { useEffect, useState } from "react";
import Loading from "../components/Loading";

async function fetchData(pageNumber: number, pageSize: number) {
  const result = await getPosts(pageNumber, pageSize);
  return result.data;
}

export default function BlogPage() {
  const [isLoading, setIsLoading] = useState(true);
  const [data, setData] = useState<Post[]>([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalCount, setTotalCount] = useState(0);
  const [nextPage, setNextPage] = useState(false);

  useEffect(() => {
    const fetchDataAndSetState = async () => {
      setIsLoading(true);
      const fetchedData = await fetchData(pageNumber, pageSize);
      setData(fetchedData.items);
      setNextPage(fetchedData.hasNextPage);
      setTotalCount(fetchedData.totalCount);
      setIsLoading(false);
    };

    fetchDataAndSetState();
  }, [pageNumber, pageSize]);

  const handlePageChange = async (newPageNumber: number) => {
    setIsLoading(true);
    const fetchedData = await fetchData(newPageNumber, pageSize);
    setData(fetchedData.items);
    setPageNumber(fetchedData.currentPage);
    setNextPage(fetchedData.hasNextPage);
    setIsLoading(false);
  };

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
                    <p className="text-base font-medium leading-6 text-dracula-dracula-500">
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
                  <button onClick={() => handlePageChange(pageNumber - 1)} className={`inline-flex text-white bg-dracula-pink hover:bg-dracula-pink-800 focus:ring-4 focus:outline-none focus:ring-dracula-pink font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:focus:ring-gray-400 my-4 transition-all duration-500 ease-in-out ${nextPage ? "rounded-l " : "rounded"}`}>
                    Anterior
                  </button>
                )}
                {nextPage && (
                  <button onClick={() => handlePageChange(pageNumber + 1)} className={`inline-flex text-white bg-dracula-pink hover:bg-dracula-pink-800 focus:ring-4 focus:outline-none focus:ring-dracula-pink font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:focus:ring-gray-400 my-4 transition-all duration-500 ease-in-out ${pageNumber > 1 ? "rounded-l " : "rounded"}`}>
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