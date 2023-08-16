"use client"

import { ProjectData } from "@/app/models";
import { createProject } from "@/app/utils/api";
import { useRouter } from "next/navigation";
import { ChangeEvent, FormEvent, useState } from "react";

export default function NewProjectPage() {
  const router = useRouter();
  const [isPublishing, setIsPublishing] = useState(false);
  const [formData, setFormData] = useState<ProjectData>({
    title: '',
    overview: '',
    url: '',
    image: null,
  })

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      setIsPublishing(true);
      const response = await createProject(formData);
      if (response) {
        setIsPublishing(false);
        router.push('/projects');
      }
    } catch (error) {
      setIsPublishing(false);
      console.error('Failed to create post:', error);
    }
  };

  const handleChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
  
    if (name === "image" && e.target instanceof HTMLInputElement && e.target.files && e.target.files.length > 0) {
      setFormData({ ...formData, [name]: e.target.files[0] });
    } else {
      setFormData({ ...formData, [name]: value });
    }
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
                placeholder="Type project title"
                required
                onChange={handleChange}
              />
            </div>

            <div className="sm:col-span-2">
              <label htmlFor="overview" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Overview</label>
              <input
                type="text"
                name="overview"
                id="overview"
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
                placeholder="Type project overview"
                required
                onChange={handleChange}
              />
            </div>

            <div className="sm:col-span-2">
              <label htmlFor="url" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Url</label>
              <input
                type="text"
                name="url"
                id="url"
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
                placeholder="Type project url"
                required
                onChange={handleChange}
              />
            </div>

            <div className="sm:col-span-2">
              <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white" htmlFor="image">Upload file</label>
              <input
                className="block w-full text-sm text-gray-900 border border-gray-300 rounded-lg cursor-pointer bg-gray-50 dark:text-gray-400 focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400"
                aria-describedby="project-image-description"
                id="image"
                type="file"
                required
                name="image"
                onChange={handleChange}
              />
            </div>

          </div>
          <button
            type="submit"
            className={`inline-flex items-center px-5 py-2.5 mt-4 sm:mt-6 text-sm font-medium text-center text-white bg-primary-700 rounded-lg focus:ring-4 bg-teal-500 hover:bg-teal-600 focus:outline-none focus:bg-teal-600 ${isPublishing ? 'opacity-50 cursor-not-allowed' : ''
              }`}
            disabled={isPublishing}
          >
            {isPublishing ? 'Publishing...' : 'Publish'}
          </button>
        </form>
      </div>
    </section>
  )
}