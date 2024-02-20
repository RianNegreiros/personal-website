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
              <label htmlFor="title" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Título</label>
              <input
                type="text"
                name="title"
                id="title"
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-dracula-pink-600 focus:border-dracula-pink-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-dracula-pink dark:focus:border-dracula-pink"
                placeholder="Título do projeto"
                required
                onChange={handleChange}
              />
            </div>

            <div className="sm:col-span-2">
              <label htmlFor="overview" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Resumo</label>
              <input
                type="text"
                name="overview"
                id="overview"
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-dracula-pink focus:border-dracula-pink block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-dracula-pink dark:focus:border-dracula-pink"
                placeholder="Visão geral do projeto"
                required
                onChange={handleChange}
              />
            </div>

            <div className="sm:col-span-2">
              <label htmlFor="url" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">URL</label>
              <input
                type="text"
                name="url"
                id="url"
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-dracula-pink-600 focus:border-dracula-pink-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-dracula-pink dark:focus:border-dracula-pink"
                placeholder="URL para o projeto, GitHub ou deploy"
                required
                onChange={handleChange}
              />
            </div>

            <div className="sm:col-span-2">
              <label
                className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                htmlFor="image"
              >
                Arquivo
              </label>
              <input
                type="file"
                id="image"
                name="image"
                className="block w-full text-sm text-gray-900 bg-gray-50 border border-gray-300 rounded-lg py-2 px-3 
            hover:shadow-md focus:ring-2 focus:ring-inset focus:ring-dracula-pink-500 focus:outline-none
            dark:bg-gray-700 dark:border-gray-600 dark:text-gray-400 dark:hover:shadow-dark"
                required
                onChange={handleChange}
              />
              <p className="mt-1 text-sm text-gray-300 dark:text-gray-300" id="file_input_help">
                SVG, PNG or JPG.
              </p>
            </div>
          </div>
          <button
            type="submit"
            className={`inline-flex items-center px-5 py-2.5 mt-4 sm:mt-6 text-sm font-medium text-center text-white rounded-lg focus:ring-4 bg-dracula-pink hover:bg-dracula-pink-500 focus:outline-none focus:bg-gray-400 ${isPublishing ? 'opacity-50 cursor-not-allowed' : ''}`}
            disabled={isPublishing}
          >
            {isPublishing ? 'Publicando...' : 'Publicar'}
          </button>
        </form >
      </div >
    </section >
  )
}