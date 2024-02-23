import { FormEvent, useState } from 'react'
import { CreateUser } from '../models'

interface CreateUserModalProps {
  onClose: () => void
  onCreateUser: (formData: CreateUser) => void
  isOpen: boolean
}

export default function AdminCreateUserModal({
  isOpen,
  onClose,
  onCreateUser,
}: CreateUserModalProps) {
  const [newUser, setNewUser] = useState<CreateUser>({
    username: '',
    email: '',
    password: '',
    admin: false,
  })

  const handleChange = (e: FormEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value, type } = e.currentTarget

    if (type === 'select-one' && name === 'admin') {
      const isAdmin = value === 'admin'
      setNewUser({ ...newUser, admin: isAdmin })
    } else {
      setNewUser({ ...newUser, [name]: value })
    }
  }

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    try {
      onCreateUser(newUser)
      setNewUser({
        username: '',
        email: '',
        password: '',
        admin: false,
      })
      onClose()
    } catch (error) {
      onClose()
      console.log('Error creating user:', error)
    }
  }

  const modalClasses = `h-screen flex justify-center items-center overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full ${isOpen ? '' : 'hidden'}`
  return (
    <div className='container mx-auto p-4'>
      <div
        id='crud-modal'
        tabIndex={-1}
        aria-hidden='true'
        className={modalClasses}
      >
        <div className='relative max-h-full w-full max-w-md p-4'>
          <div className='relative rounded-lg bg-white shadow dark:bg-gray-700'>
            <div className='flex items-center justify-between rounded-t border-b p-4 dark:border-gray-600 md:p-5'>
              <h3 className='text-lg font-semibold text-gray-900 dark:text-white'>
                Create New User
              </h3>
              <button
                type='button'
                onClick={() => onClose()}
                className='ms-auto inline-flex h-8 w-8 items-center justify-center rounded-lg bg-transparent text-sm text-gray-400 hover:bg-gray-200 hover:text-gray-900 dark:hover:bg-gray-600 dark:hover:text-white'
                data-modal-toggle='crud-modal'
              >
                <svg
                  className='h-3 w-3'
                  aria-hidden='true'
                  xmlns='http://www.w3.org/2000/svg'
                  fill='none'
                  viewBox='0 0 14 14'
                >
                  <path
                    stroke='currentColor'
                    strokeLinecap='round'
                    strokeLinejoin='round'
                    strokeWidth='2'
                    d='m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6'
                  ></path>
                </svg>
                <span className='sr-only'>Close modal</span>
              </button>
            </div>

            <form className='p-4 md:p-5' onSubmit={handleSubmit}>
              <div className='mb-4 grid grid-cols-2 gap-4'>
                <div className='col-span-2'>
                  <label
                    htmlFor='username'
                    className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
                  >
                    Username
                  </label>
                  <input
                    type='text'
                    name='username'
                    id='username'
                    value={newUser.username}
                    onChange={handleChange}
                    className='focus:ring-primary-600 focus:border-primary-600 dark:focus:ring-primary-500 dark:focus:border-primary-500 block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 dark:border-gray-500 dark:bg-gray-600 dark:text-white dark:placeholder-gray-400'
                    placeholder='Type username'
                    required
                  />
                </div>
                <div className='col-span-2'>
                  <label
                    htmlFor='email'
                    className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
                  >
                    Email
                  </label>
                  <input
                    type='email'
                    name='email'
                    id='email'
                    value={newUser.email}
                    onChange={handleChange}
                    className='focus:ring-primary-600 focus:border-primary-600 dark:focus:ring-primary-500 dark:focus:border-primary-500 block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 dark:border-gray-500 dark:bg-gray-600 dark:text-white dark:placeholder-gray-400'
                    placeholder='Type email'
                    required
                  />
                </div>
                <div className='col-span-2'>
                  <label
                    htmlFor='password'
                    className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
                  >
                    Password
                  </label>
                  <input
                    type='password'
                    name='password'
                    id='password'
                    value={newUser.password}
                    onChange={handleChange}
                    className='focus:ring-primary-600 focus:border-primary-600 dark:focus:ring-primary-500 dark:focus:border-primary-500 block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 dark:border-gray-500 dark:bg-gray-600 dark:text-white dark:placeholder-gray-400'
                    placeholder='Type password'
                    required
                  />
                </div>
                <div className='col-span-2'>
                  <label
                    htmlFor='admin'
                    className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
                  >
                    Role
                  </label>
                  <select
                    id='admin'
                    name='admin'
                    value={newUser.admin ? 'admin' : 'user'}
                    onChange={handleChange}
                    className='focus:ring-primary-500 focus:border-primary-500 dark:focus:ring-primary-500 dark:focus:border-primary-500 block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 dark:border-gray-500 dark:bg-gray-600 dark:text-white dark:placeholder-gray-400'
                  >
                    <option value='user'>User</option>
                    <option value='admin'>Admin</option>
                  </select>
                </div>
              </div>
              <button
                type='submit'
                className='inline-flex items-center rounded-lg bg-cyan-700 px-5 py-2.5 text-center text-sm font-medium text-white hover:bg-cyan-800 focus:outline-none focus:ring-4 focus:ring-cyan-300 dark:bg-cyan-600 dark:hover:bg-cyan-700 dark:focus:ring-cyan-800'
              >
                <svg
                  className='-ms-1 me-1 h-5 w-5'
                  fill='currentColor'
                  viewBox='0 0 20 20'
                  xmlns='http://www.w3.org/2000/svg'
                >
                  <path
                    fillRule='evenodd'
                    d='M10 5a1 1 0 011 1v3h3a1 1 0 110 2h-3v3a1 1 0 11-2 0v-3H6a1 1 0 110-2h3V6a1 1 0 011-1z'
                    clipRule='evenodd'
                  ></path>
                </svg>
                Add new user
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  )
}
