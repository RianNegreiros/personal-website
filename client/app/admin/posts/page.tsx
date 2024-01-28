"use client"

import AdminDeleteModal from "@/app/components/AdminDeleteModal";
import { deleteAdminPost, getAdminPosts } from "@/app/utils/api";
import Link from "next/link";
import { useEffect, useState } from "react";

async function fetchData() {
  const result = await getAdminPosts();
  return result;
}

interface AdminPost {
  author: { id: string, userName: string };
  id: string
  title: string
  summary: string
}

export default function PostsPage() {
  const [data, setData] = useState<AdminPost[]>([]);

  useEffect(() => {
    async function getData() {
      const posts = await fetchData();
      setData(posts);
    }
    getData();
  }, []);

  console.log(data)

  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [selectedPostId, setSelectedPostId] = useState<string>('');

  const handleDeleteCancel = () => {
    setIsDeleteModalOpen(false);
    setSelectedPostId('');
  };

  const handleDeleteConfirm = async () => {
    setIsDeleteModalOpen(false);
    await deleteAdminPost(selectedPostId);
  };

  const openDeleteModal = (postId: string) => {
    setIsDeleteModalOpen(true);
    setSelectedPostId(postId);
  };

  return (
    <>
      <section className="bg-gray-50 dark:bg-gray-900 p-3 sm:p-5 antialiased">
        <div className="mx-auto max-w-screen-2xl px-4 lg:px-12">
          <div className="bg-white dark:bg-gray-800 relative shadow-md sm:rounded-lg overflow-hidden">
            <div className="flex flex-col md:flex-row md:items-center md:justify-between space-y-3 md:space-y-0 md:space-x-4 p-4">
              <div className="flex-1 flex items-center space-x-2">
                <h5>
                  <span className="text-gray-500">All Posts: </span>
                  <span className="dark:text-white">{data.length}</span>
                </h5>
              </div>
            </div>
            <div className="flex flex-col md:flex-row items-stretch md:items-center md:space-x-3 space-y-3 md:space-y-0 justify-between mx-4 py-4 border-t dark:border-gray-700">
              <div className="w-full md:w-1/2">
                <form className="flex items-center">
                  <label htmlFor="simple-search" className="sr-only">
                    Search
                  </label>
                  <div className="relative w-full">
                    <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                      <svg
                        aria-hidden="true"
                        className="w-5 h-5 text-gray-500 dark:text-gray-400"
                        fill="currentColor"
                        viewBox="0 0 20 20"
                        xmlns="http://www.w3.org/2000/svg"
                      >
                        <path
                          fillRule="evenodd"
                          clipRule="evenodd"
                          d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z"
                        />
                      </svg>
                    </div>
                    <input
                      type="text"
                      id="simple-search"
                      placeholder="Search for posts"
                      required
                      className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full pl-10 p-2 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
                    />
                  </div>
                </form>
              </div>
            </div>

            <div className="overflow-x-auto">
              <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
                <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                  <tr>
                    <th scope="col" className="p-4">
                      Id
                    </th>
                    <th scope="col" className="p-4">
                      Title
                    </th>
                    <th scope="col" className="p-4">
                      Author
                    </th>
                    <th scope="col" className="p-4">
                      Summary
                    </th>
                    <th scope="col" className="p-4">
                      Actions
                    </th>
                  </tr>
                </thead>
                <tbody>
                  {data.map((post) => (
                    <tr key={post.id} className="border-b dark:border-gray-600 hover:bg-gray-100 dark:hover:bg-gray-700">
                      <th scope="row" className="px-4 py-3 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                        <div className="flex items-center mr-3">{post.id}</div>
                      </th>
                      <td className="px-4 py-3">{post.title}</td>
                      <td className="px-4 py-3 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                        <div className="flex items-center">{post.author.userName}</div>
                      </td>
                      <td className="px-4 py-3">{post.summary}</td>
                      <td className="px-4 py-3 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                        <div className="flex items-center space-x-4">
                          <Link
                            href={`/edit-post/${post.id}`}
                            className="py-2 px-3 flex items-center text-sm font-medium text-center text-white bg-primary-700 rounded-lg hover:bg-primary-800 focus:ring-4 focus:outline-none focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800"
                          >
                            Edit
                          </Link>
                          <button
                            type="button"
                            onClick={() => openDeleteModal(post.id)}
                            className="py-2 px-3 flex items-center text-sm font-medium text-center text-white bg-red-700 rounded-lg hover:bg-red-800 focus:ring-4 focus:outline-none focus:ring-red-300 dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-800"
                          >
                            Delete
                          </button>
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </section>

      {isDeleteModalOpen && (
        <AdminDeleteModal onCancel={handleDeleteCancel} onDelete={handleDeleteConfirm} isOpen={isDeleteModalOpen} />
      )}
    </>
  );
}
