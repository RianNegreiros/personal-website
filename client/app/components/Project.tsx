import Image from 'next/image'
import Link from 'next/link'
import { Project } from '../models'

interface ProjectProps {
  project: Project
}

export default function Projects({ project }: ProjectProps) {
  return (
    <article
      key={project.id}
      className='overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg shadow-dracula-purple-100 dark:border-zinc-600 dark:bg-black dark:shadow-gray-700'
    >
      <div className='relative h-56 w-full'>
        <Image
          fill
          sizes='100%'
          priority
          src={project.imageUrl}
          alt='Imagem do projeto'
          className='h-full w-full object-cover'
        />
      </div>

      <div className='p-4 sm:p-6'>
        <Link
          href={project.url}
          target='_blank'
          aria-label='Abrir em outra página GitHub do projeto'
        >
          <h3 className='text-lg font-medium text-gray-900 dark:text-white'>
            {project.title}
          </h3>
        </Link>

        <p className='mt-2 line-clamp-3 text-sm leading-relaxed text-gray-500 dark:text-gray-400'>
          {project.overview}
        </p>

        <Link
          href={project.url}
          target='_blank'
          aria-label='Abrir em outra página GitHub do projeto'
          className='group mt-4 inline-flex cursor-pointer items-center gap-1 text-sm font-medium text-dracula-purple'
        >
          Saiba mais
          <span className='block transition-all group-hover:ms-0.5'>
            &rarr;
          </span>
        </Link>
      </div>
    </article>
  )
}
