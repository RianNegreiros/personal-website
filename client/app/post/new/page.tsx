"use client"

import { PostData } from "@/app/models";
import { createPost } from "@/app/utils/api";
import { useRouter } from "next/navigation";
import { ChangeEvent, FormEvent, useState } from "react";

export default function NewPostPage() {
  const router = useRouter();
  const [formData, setFormData] = useState<PostData>({
    authorId: '',
    title: '',
    summary: '',
    content: '',
  });

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      formData.authorId = localStorage.getItem('userId') as string;
      const response = await createPost(formData);
      if (response) {
        router.push('/');
      }
    } catch (error) {
      console.error('Failed to create post:', error);
    }
  };

  const handleChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({ ...prevData, [name]: value }));
  };

  return (
    <section className="bg-white dark:bg-gray-900">
      <div className="py-8 px-4 mx-auto max-w-2xl lg:py-16">
        <form onSubmit={handleSubmit}>
          <div className="grid gap-4 sm:grid-cols-2 sm:gap-6">
            <div className="sm:col-span-2">
              <label htmlFor="title" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Title</label>
              <input
                type="text"
                name="title"
                id="title"
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
                placeholder="Type post title"
                required
                onChange={handleChange}
              />
            </div>

            <div className="sm:col-span-2">
              <label htmlFor="summary" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Summary</label>
              <input
                type="text"
                name="summary"
                id="summary"
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
                placeholder="Type post summary"
                required
                onChange={handleChange}
              />
            </div>

            <div className="sm:col-span-2">
              <label htmlFor="content" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Content</label>
              <textarea
                id="content"
                rows={16}
                className="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-primary-500 focus:border-primary-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
                placeholder="Place the markdown text here"
                name="content"
                required
                onChange={handleChange}
              ></textarea>
            </div>
          </div>
          <button type="submit" className="inline-flex items-center px-5 py-2.5 mt-4 sm:mt-6 text-sm font-medium text-center text-white bg-primary-700 rounded-lg focus:ring-4 bg-teal-500 hover:bg-teal-600 focus:outline-none focus:bg-teal-600">
            Publish
          </button>
        </form>
      </div>
    </section>
  )
}