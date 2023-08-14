"use client"

import Link from "next/link";
import { Post } from "./models";
import { getPosts } from "./utils/api";
import { useEffect, useState } from "react";

async function getData() {
  return await getPosts();
}

export default function BlogPage() {
  const [data, setData] = useState<Post[]>([]);
  const [sortBy, setSortBy] = useState<"newest" | "oldest">("newest");

  useEffect(() => {
    async function fetchData() {
      const fetchedData = await getData();
      setData(fetchedData);
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
    <div className="divide-y divide-gray-200 dark:divide-gray-700">
      <div className="space-y-2 pt-6 pb-8 md:space-y-5">
        <div>
          <label htmlFor="sortSelect" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
          Ordenar por:
          </label>
          <select
            id="sortSelect"
            className="mt-1 block w-full p-2 border-gray-300 dark:border-gray-700 bg-white dark:bg-gray-800 shadow-sm focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md"
            onChange={(e) => handleSortChange(e.target.value as "newest" | "oldest")}
            value={sortBy}
          >
            <option value="newest">Mais recente</option>
            <option value="oldest">Mais antigo</option>
          </select>
        </div>
      </div>

      <ul>
        {sortedData.map((post) => (
          <li key={post.id} className="py-4">
            <article className="space-y-2 xl:grid xl:grid-cols-4 xl:items-baseline xl:space-y-0">
              <div>
                <p className="text-base font-medium leading-6 text-teal-500">
                  {new Date(post.createdAt.split('.')[0]).toLocaleDateString()}
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
  )
}