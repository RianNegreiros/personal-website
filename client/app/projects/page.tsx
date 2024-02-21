'use client'

import Image from 'next/image'
import { getProjects } from '../utils/api'
import { Project } from '../models'
import { useEffect, useState } from 'react'
import Link from 'next/link'
import Loading from '../components/Loading'

async function getData() {
  return await getProjects()
}

export default function Projects() {
  const [isLoading, setIsLoading] = useState(true)
  const [data, setData] = useState<Project[]>([])

  useEffect(() => {
    async function fetchData() {
      const projects = await getData()
      setData(projects.data)
      setIsLoading(false)
    }
    fetchData()
  }, [])

  return (
    <div className='divide-y divide-gray-200 dark:divide-gray-700'>
      <div className='space-y-2 pb-8 pt-6 md:space-y-5'></div>
      {isLoading ? (
        <Loading />
      ) : (
        <div className='grid gap-y-8 pt-8 sm:grid-cols-2 sm:gap-6 md:gap-6 lg:grid-cols-3 lg:gap-10'>
          {data.map((project) => (
            <article
              key={project.id}
              className='overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg shadow-dracula-pink-100 dark:border-zinc-600 dark:bg-black dark:shadow-gray-700'
            >
              <div className='relative h-56 w-full'>
                <Image
                  fill
                  sizes='100%'
                  priority
                  src={project.imageUrl}
                  alt='Image of the project'
                  className='h-full w-full object-cover'
                />
              </div>

              <div className='p-4 sm:p-6'>
                <Link href={project.url} target='_blank'>
                  <h3 className='text-lg font-medium text-gray-900 dark:text-white'>
                    {project.title}
                  </h3>
                </Link>

                <p className=' mt-2 line-clamp-3 text-sm leading-relaxed text-gray-500 dark:text-gray-400'>
                  {project.overview}
                </p>

                <Link
                  href={project.url}
                  target='_blank'
                  className='group mt-4 inline-flex cursor-pointer items-center gap-1 text-sm font-medium text-dracula-pink'
                >
                  Saiba mais
                  <span className='block transition-all group-hover:ms-0.5'>
                    &rarr;
                  </span>
                </Link>
              </div>
            </article>
          ))}
        </div>
      )}
    </div>
  )
}
