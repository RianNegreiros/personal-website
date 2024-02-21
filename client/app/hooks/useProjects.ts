'use client'

import { useState, useEffect } from 'react'
import { getProjects } from '@/app/utils/api'
import { Project } from '../models'

export function useProjects() {
  const [isLoading, setIsLoading] = useState(true)
  const [projects, setProjects] = useState<Project[]>([])

  useEffect(() => {
    getProjects()
      .then((projects) => {
        setProjects(projects.data)
        setIsLoading(false)
      })
      .catch((error) => {
        console.error(error)
        setIsLoading(false)
      })
  }, [])

  return { isLoading, projects }
}
