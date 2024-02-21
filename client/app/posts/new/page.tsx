'use client'

import { PostData } from '@/app/models'
import { createPost } from '@/app/utils/api'
import { useRouter } from 'next/navigation'
import { ChangeEvent, FormEvent, useState } from 'react'

export default function NewPostPage() {
  const router = useRouter()
  const [isPublishing, setIsPublishing] = useState(false)
  const [formData, setFormData] = useState<PostData>({
    authorId: '',
    title: '',
    summary: '',
    content: '',
  })

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault()

    try {
      setIsPublishing(true)
      const response = await createPost(formData)
      if (response) {
        setIsPublishing(false)
        router.push('/')
      }
    } catch (error) {
      console.error('Failed to create post:', error)
      setIsPublishing(false)
    }
  }

  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = e.target
    setFormData((prevData) => ({ ...prevData, [name]: value }))
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
                Title
              </label>
              <input
                type='text'
                name='title'
                id='title'
                className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-dracula-pink-600 focus:ring-dracula-pink dark:border-gray-600 dark:bg-gray-700 dark:text-dracula-pink dark:placeholder-gray-400 dark:focus:border-dracula-pink dark:focus:ring-dracula-pink'
                placeholder='Type post title'
                required
                onChange={handleChange}
              />
            </div>

            <div className='sm:col-span-2'>
              <label
                htmlFor='summary'
                className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
              >
                Summary
              </label>
              <input
                type='text'
                name='summary'
                id='summary'
                className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-dracula-pink-600 focus:ring-dracula-pink-600 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-dracula-pink dark:focus:ring-dracula-pink'
                placeholder='Type post summary'
                required
                onChange={handleChange}
              />
            </div>

            <div className='sm:col-span-2'>
              <label
                htmlFor='content'
                className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
              >
                Content
              </label>
              <textarea
                id='content'
                rows={16}
                className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-dracula-pink focus:ring-dracula-pink dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-dracula-pink dark:focus:ring-dracula-pink'
                placeholder='Place the markdown text here'
                name='content'
                required
                onChange={handleChange}
              ></textarea>
            </div>
          </div>
          <button
            type='submit'
            className={`mt-4 inline-flex items-center rounded-lg bg-dracula-pink-700 px-5 py-2.5 text-center text-sm font-medium text-white hover:bg-dracula-pink-600 focus:bg-dracula-pink-600 focus:outline-none focus:ring-4 sm:mt-6 ${
              isPublishing ? 'cursor-not-allowed opacity-50' : ''
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
