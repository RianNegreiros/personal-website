"use client"

import { PostData } from "@/app/models";
import { createPost } from "@/app/utils/api";
import { useRouter } from "next/navigation";
import { ChangeEvent, FormEvent, useState } from "react";

export default function NewPostPage() {
  const router = useRouter();
  const [isPublishing, setIsPublishing] = useState(false);
  const [formData, setFormData] = useState<PostData>({
    authorId: '',
    title: '',
    summary: '',
    content: '',
  });

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      setIsPublishing(true);
      const response = await createPost(formData);
      if (response) {
        setIsPublishing(false);
        router.push('/');
      }
    } catch (error) {
      console.error('Failed to create post:', error);
      setIsPublishing(false);
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
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-dracula-pink focus:border-dracula-pink-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-dracula-pink dark:focus:ring-dracula-pink dark:focus:border-dracula-pink"
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
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-dracula-pink-600 focus:border-dracula-pink-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-dracula-pink dark:focus:border-dracula-pink"
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
                className="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-dracula-pink focus:border-dracula-pink dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-dracula-pink dark:focus:border-dracula-pink"
                placeholder="Place the markdown text here"
                name="content"
                required
                onChange={handleChange}
              ></textarea>
            </div>
          </div>
          <button
            type="submit"
            className={`inline-flex items-center px-5 py-2.5 mt-4 sm:mt-6 text-sm font-medium text-center text-white rounded-lg focus:ring-4 bg-dracula-pink-700 hover:bg-dracula-pink-600 focus:outline-none focus:bg-dracula-pink-600 ${isPublishing ? 'opacity-50 cursor-not-allowed' : ''
              }`}
            disabled={isPublishing}
          >
            {isPublishing ? 'Publicando...' : 'Publicar'}
          </button>
        </form>
      </div>
    </section>
  )
}