'use client'

import { ProjectData } from '@/app/models'
import { createProject } from '@/app/utils/api'
import { useRouter } from 'next/navigation'
import { ChangeEvent, FormEvent, useState } from 'react'

export default function NewProjectPage() {
  const router = useRouter()
  const [isPublishing, setIsPublishing] = useState(false)
  const [formData, setFormData] = useState<ProjectData>({
    title: '',
    overview: '',
    url: '',
    image: null,
  })

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault()

    try {
      setIsPublishing(true)
      const response = await createProject(formData)
      if (response) {
        setIsPublishing(false)
        router.push('/projects')
      }
    } catch (error) {
      setIsPublishing(false)
      console.error('Failed to create post:', error)
    }
  }

  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = e.target

    if (
      name === 'image' &&
      e.target instanceof HTMLInputElement &&
      e.target.files &&
      e.target.files.length > 0
    ) {
      setFormData({ ...formData, [name]: e.target.files[0] })
    } else {
      setFormData({ ...formData, [name]: value })
    }
  }

  return (
    <section className='bg-white dark:bg-gray-900'>
      <div className='mx-auto max-w-2xl px-4 py-8 lg:py-16'>
        <form onSubmit={handleSubmit}>
          <div className='grid gap-4 sm:grid-cols-2 sm:gap-6'>
            <div className='sm:col-span-2'>
              <label
                htmlFor='title'
                className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
              >
                Título
              </label>
              <input
                type='text'
                name='title'
                id='title'
                className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-dracula-pink-600 focus:ring-dracula-pink-600 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-dracula-pink dark:focus:ring-dracula-pink'
                placeholder='Título do projeto'
                required
                onChange={handleChange}
              />
            </div>

            <div className='sm:col-span-2'>
              <label
                htmlFor='overview'
                className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
              >
                Resumo
              </label>
              <input
                type='text'
                name='overview'
                id='overview'
                className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-dracula-pink focus:ring-dracula-pink dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-dracula-pink dark:focus:ring-dracula-pink'
                placeholder='Visão geral do projeto'
                required
                onChange={handleChange}
              />
            </div>

            <div className='sm:col-span-2'>
              <label
                htmlFor='url'
                className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
              >
                URL
              </label>
              <input
                type='text'
                name='url'
                id='url'
                className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-dracula-pink-600 focus:ring-dracula-pink-600 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-dracula-pink dark:focus:ring-dracula-pink'
                placeholder='URL para o projeto, GitHub ou deploy'
                required
                onChange={handleChange}
              />
            </div>

            <div className='sm:col-span-2'>
              <label
                className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
                htmlFor='image'
              >
                Arquivo
              </label>
              <input
                type='file'
                id='image'
                name='image'
                className='dark:hover:shadow-dark block w-full rounded-lg border border-gray-300 bg-gray-50 px-3 py-2 text-sm 
            text-gray-900 hover:shadow-md focus:outline-none focus:ring-2 focus:ring-inset
            focus:ring-dracula-pink-500 dark:border-gray-600 dark:bg-gray-700 dark:text-gray-400'
                required
                onChange={handleChange}
              />
              <p
                className='mt-1 text-sm text-gray-300 dark:text-gray-300'
                id='file_input_help'
              >
                SVG, PNG or JPG.
              </p>
            </div>
          </div>
          <button
            type='submit'
            className={`mt-4 inline-flex items-center rounded-lg bg-dracula-pink px-5 py-2.5 text-center text-sm font-medium text-white hover:bg-dracula-pink-500 focus:bg-gray-400 focus:outline-none focus:ring-4 sm:mt-6 ${isPublishing ? 'cursor-not-allowed opacity-50' : ''}`}
            disabled={isPublishing}
          >
            {isPublishing ? 'Publicando...' : 'Publicar'}
          </button>
        </form>
      </div>
    </section>
  )
}
