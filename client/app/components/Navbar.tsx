'use client'

import Link from 'next/link'
import ThemeButton from './ThemeButton'
import { Disclosure } from '@headlessui/react'
import { usePathname } from 'next/navigation'
import NewLinks from './NewLinks'
import LogoutLink from './LogoutLink'

export default function Navbar() {
  let pathname = usePathname() || '/'

  return (
    <Disclosure as='nav'>
      {({ open }) => (
        <>
          <div className='mx-auto max-w-6xl px-4 sm:px-6 lg:px-8'>
            <div className='flex h-16 justify-between'>
              <div className='flex w-full justify-between'>
                <div className='flex items-center sm:ml-6 sm:flex sm:items-center sm:space-x-8'>
                  <Link href='/'>
                    <h1 className='text-2xl font-medium'>
                      Rian{' '}
                      <span className='text-dracula-purple'>Negreiros</span>
                    </h1>
                  </Link>

                  <Link
                    href='/'
                    prefetch
                    className={`${pathname === '/' ? 'inline-flex h-full items-center border-b-2 border-dracula-purple px-1 pt-1 text-sm font-medium dark:text-white' : 'inline-flex items-center border-b-2 border-transparent px-1 pt-1 text-sm font-medium text-gray-500 dark:text-gray-300 dark:hover:text-white'} transition duration-300 ease-in-out`}
                  >
                    Home
                  </Link>

                  <Link
                    href='/posts'
                    prefetch
                    className={`${pathname === '/posts' ? 'inline-flex h-full items-center border-b-2 border-dracula-purple px-1 pt-1 text-sm font-medium dark:text-white' : 'inline-flex items-center border-b-2 border-transparent px-1 pt-1 text-sm font-medium text-gray-500 dark:text-gray-300 dark:hover:text-white'} transition duration-300 ease-in-out`}
                  >
                    Blog
                  </Link>

                  <Link
                    href='/projects'
                    prefetch
                    className={`${pathname === '/projects' ? 'inline-flex h-full items-center border-b-2 border-dracula-purple px-1 pt-1 text-sm font-medium dark:text-white' : 'inline-flex items-center border-b-2 border-transparent px-1 pt-1 text-sm font-medium text-gray-500 dark:text-gray-300 dark:hover:text-white'} transition duration-300 ease-in-out`}
                  >
                    Projetos
                  </Link>
                </div>

                <div className='hidden sm:ml-6 sm:flex sm:items-center sm:space-x-8'>
                  <NewLinks pathname={pathname} />
                  <LogoutLink pathname={pathname} />
                  <ThemeButton />
                </div>
              </div>

              <div className='-mr-2 flex items-center sm:hidden'>
                <ThemeButton />

                <Disclosure.Button
                  id='mobile-menu'
                  className='inline-flex items-center justify-center rounded-md p-2 text-gray-400 hover:bg-gray-100 hover:text-gray-500 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-dracula-purple dark:hover:bg-gray-800'
                >
                  {open ? (
                    <svg
                      xmlns='http://www.w3.org/2000/svg'
                      fill='none'
                      viewBox='0 0 24 24'
                      strokeWidth={1.5}
                      stroke='currentColor'
                      className='h-6 w-6'
                    >
                      <path
                        strokeLinecap='round'
                        strokeLinejoin='round'
                        d='M6 18L18 6M6 6l12 12'
                      />
                    </svg>
                  ) : (
                    <svg
                      xmlns='http://www.w3.org/2000/svg'
                      fill='none'
                      viewBox='0 0 24 24'
                      strokeWidth={1.5}
                      stroke='currentColor'
                      className='h-6 w-6'
                    >
                      <path
                        strokeLinecap='round'
                        strokeLinejoin='round'
                        d='M3.75 6.75h16.5M3.75 12h16.5m-16.5 5.25h16.5'
                      />
                    </svg>
                  )}
                </Disclosure.Button>
              </div>
            </div>
          </div>

          <Disclosure.Panel className='sm:hidden'>
            <div className='space-y-1 pb-3 pt-2'>
              <Link
                href='/'
                prefetch
                className={`${
                  pathname === '/'
                    ? 'block border-l-4 border-dracula-purple bg-dracula-purple-50 py-2 pl-3 pr-4 text-base font-medium text-dracula-purple dark:bg-gray-800'
                    : 'block border-l-4 border-transparent py-2 pl-3 pr-4 text-base font-medium text-gray-500 hover:border-gray-300 hover:bg-dracula-purple-50 hover:text-dracula-purple dark:text-white dark:hover:bg-gray-700'
                }`}
              >
                Home
              </Link>

              <Link
                href='/posts'
                prefetch
                className={`${
                  pathname === '/posts'
                    ? 'block border-l-4 border-dracula-purple bg-dracula-purple-50 py-2 pl-3 pr-4 text-base font-medium text-dracula-purple dark:bg-gray-800'
                    : 'block border-l-4 border-transparent py-2 pl-3 pr-4 text-base font-medium text-gray-500 hover:border-gray-300 hover:bg-dracula-purple-50 hover:text-dracula-purple dark:text-white dark:hover:bg-gray-700'
                }`}
              >
                Blog
              </Link>

              <Link
                href='/projects'
                prefetch
                className={`${
                  pathname === '/projects'
                    ? 'block border-l-4 border-dracula-purple bg-dracula-purple-50 py-2 pl-3 pr-4 text-base font-medium text-dracula-purple dark:bg-gray-800'
                    : 'block border-l-4 border-transparent py-2 pl-3 pr-4 text-base font-medium text-gray-500 hover:border-gray-300 hover:bg-dracula-purple-50 hover:text-dracula-purple dark:text-white dark:hover:bg-gray-700'
                }`}
              >
                Projetos
              </Link>
            </div>
          </Disclosure.Panel>
        </>
      )}
    </Disclosure>
  )
}
