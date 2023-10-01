"use client"

import Link from "next/link";
import { Post } from "./models";
import { getPosts } from "./utils/api";
import { useEffect, useState } from "react";
import Loading from "./components/Loading";

async function getData() {
  return await getPosts();
}

export default function BlogPage() {
  const [data, setData] = useState<Post[]>([]);
  const [sortBy, setSortBy] = useState<"newest" | "oldest">("newest");
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    async function fetchData() {
      const fetchedData = await getData();
      setData(fetchedData.data);
      setIsLoading(false);
    }
    fetchData();
  }, []);

  const handleSortChange = (value: "newest" | "oldest") => {
    setSortBy(value);
  };

  const sortedData = data.slice().sort((a, b) => {
    if (sortBy === "oldest") {
      return a.createdAt.localeCompare(b.createdAt);
    } else {
      return b.createdAt.localeCompare(a.createdAt);
    }
  });

  return (
    <>
      {isLoading ? (
        <Loading />
      ) : (
        <div className="divide-y divide-gray-200 dark:divide-gray-700">
          <div className="space-y-2 pt-6 pb-8 md:space-y-5">
            <div>
              <label htmlFor="sortButton" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                Ordenar por:
              </label>
              <button
                id="sortButton"
                className="mt-2 flex items-center space-x-1 p-2 border-gray-300 dark:border-gray-700 bg-white dark:bg-gray-800 shadow-sm focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md"
                onClick={() => handleSortChange(sortBy === "newest" ? "oldest" : "newest")}
              >
                <svg className="mr-1" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 320 512">
                  <path d="M41 288h238c21.4 0 32.1 25.9 17 41L177 448c-9.4 9.4-24.6 9.4-33.9 0L24 329c-15.1-15.1-4.4-41 17-41zm255-105L177 64c-9.4-9.4-24.6-9.4-33.9 0L24 183c-15.1 15.1-4.4 41 17 41h238c21.4 0 32.1-25.9 17-41z" />
                </svg>
                <span>{sortBy === "newest" ? "Mais recente" : "Mais antigo"}</span>
              </button>
            </div>
          </div>

          <ul>
            {sortedData.map((post) => (
              <li key={post.id} className="py-4">
                <article className="space-y-2 xl:grid xl:grid-cols-4 xl:items-baseline xl:space-y-0">
                  <div>
                    <p className="text-base font-medium leading-6 text-teal-500">
                      {new Date(post.createdAt).toLocaleDateString()}
                    </p>
                  </div>

                  <Link href={`/post/${post.slug}`}
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
          </ul>
        </div>
      )}
    </>
  )
}