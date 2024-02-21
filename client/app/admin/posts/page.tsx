'use client'

import AdminDeleteModal from '@/app/components/AdminDeleteModal'
import InternalServerError from '@/app/components/InternalServerError'
import Loading from '@/app/components/Loading'
import Unauthorized from '@/app/components/Unauthorized'
import { useAuth } from '@/app/contexts/AuthContext'
import { useAdminPosts } from '@/app/hooks/useAdminPosts'
import { deleteAdminPost } from '@/app/utils/api'
import Link from 'next/link'
import { useState } from 'react'

export default function PostsPage() {
  const { isAdmin } = useAuth()
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false)
  const [selectedPostId, setSelectedPostId] = useState<string>('')
  const { data, isLoading, error } = useAdminPosts()

  if (!isAdmin) {
    return (
      <Unauthorized errorMessage='Você não tem permissão para acessar esta página.' />
    )
  }

  if (isLoading) {
    return <Loading />
  }

  if (error) {
    return <InternalServerError errorMessage={error.message} />
  }

  const handleDeleteCancel = () => {
    setIsDeleteModalOpen(false)
    setSelectedPostId('')
  }

  const handleDeleteConfirm = async () => {
    setIsDeleteModalOpen(false)
    await deleteAdminPost(selectedPostId)
  }

  const openDeleteModal = (postId: string) => {
    setIsDeleteModalOpen(true)
    setSelectedPostId(postId)
  }

  return (
    <>
      <section className='bg-gray-50 p-3 antialiased dark:bg-gray-900 sm:p-5'>
        <div className='mx-auto max-w-screen-2xl px-4 lg:px-12'>
          <div className='relative overflow-hidden bg-white shadow-md dark:bg-gray-800 sm:rounded-lg'>
            <div className='flex flex-col space-y-3 p-4 md:flex-row md:items-center md:justify-between md:space-x-4 md:space-y-0'>
              <div className='flex flex-1 items-center space-x-2'>
                <h5>
                  <span className='text-gray-500'>All Posts: </span>
                  <span className='dark:text-white'>{data.length}</span>
                </h5>
              </div>
            </div>

            <div className='overflow-x-auto'>
              <table className='w-full text-left text-sm text-gray-500 dark:text-gray-400'>
                <thead className='bg-gray-50 text-xs uppercase text-gray-700 dark:bg-gray-700 dark:text-gray-400'>
                  <tr>
                    <th scope='col' className='p-4'>
                      Id
                    </th>
                    <th scope='col' className='p-4'>
                      Title
                    </th>
                    <th scope='col' className='p-4'>
                      Author
                    </th>
                    <th scope='col' className='p-4'>
                      Summary
                    </th>
                    <th scope='col' className='p-4'>
                      Actions
                    </th>
                  </tr>
                </thead>
                <tbody>
                  {data.map((post) => (
                    <tr
                      key={post.id}
                      className='border-b hover:bg-gray-100 dark:border-gray-600 dark:hover:bg-gray-700'
                    >
                      <th
                        scope='row'
                        className='whitespace-nowrap px-4 py-3 font-medium text-gray-900 dark:text-white'
                      >
                        <div className='mr-3 flex items-center'>{post.id}</div>
                      </th>
                      <td className='px-4 py-3'>{post.title}</td>
                      <td className='whitespace-nowrap px-4 py-3 font-medium text-gray-900 dark:text-white'>
                        <div className='flex items-center'>
                          {post.author.userName}
                        </div>
                      </td>
                      <td className='px-4 py-3'>
                        {post.summary.length > 100
                          ? post.summary.substring(0, 100) + '...'
                          : post.summary}
                      </td>{' '}
                      <td className='whitespace-nowrap px-4 py-3 font-medium text-gray-900 dark:text-white'>
                        <div className='flex items-center space-x-4'>
                          <Link
                            href={`/edit-post/${post.id}`}
                            className='bg-primary-700 hover:bg-primary-800 focus:ring-primary-300 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 flex items-center rounded-lg px-3 py-2 text-center text-sm font-medium text-white focus:outline-none focus:ring-4'
                          >
                            Edit
                          </Link>
                          <button
                            type='button'
                            onClick={() => openDeleteModal(post.id)}
                            className='flex items-center rounded-lg bg-red-700 px-3 py-2 text-center text-sm font-medium text-white hover:bg-red-800 focus:outline-none focus:ring-4 focus:ring-red-300 dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-800'
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
        <AdminDeleteModal
          onCancel={handleDeleteCancel}
          onDelete={handleDeleteConfirm}
          isOpen={isDeleteModalOpen}
        />
      )}
    </>
  )
}
