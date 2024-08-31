/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface Animal {
  /** @format int32 */
  id?: number;
  /** @minLength 1 */
  name: string;
  /** @minLength 1 */
  breed: string;
  /** @format byte */
  photo?: string | null;
  /**
   * @format int32
   * @min 0
   * @max 2147483647
   */
  age?: number | null;
  description?: string | null;
  /** @format int32 */
  animalShelterId?: number;
  animalShelter?: AnimalShelter;
}

export interface AnimalShelter {
  /** @format int32 */
  id?: number;
  /** @minLength 1 */
  name: string;
  /** @minLength 1 */
  address: string;
  /**
   * @format int32
   * @min 0
   * @max 2147483647
   */
  capacity: number;
  contactEmail?: string | null;
  contactPhone?: string | null;
  website?: string | null;
  /** @format date-time */
  establishedDate?: string;
  description?: string | null;
  personId?: string | null;
  admins?: ApplicationUser[] | null;
  workers?: ApplicationUser[] | null;
}

export interface ApplicationUser {
  id?: string | null;
  userName?: string | null;
  normalizedUserName?: string | null;
  normalizedEmail?: string | null;
  emailConfirmed?: boolean;
  passwordHash?: string | null;
  securityStamp?: string | null;
  concurrencyStamp?: string | null;
  phoneNumberConfirmed?: boolean;
  twoFactorEnabled?: boolean;
  /** @format date-time */
  lockoutEnd?: string | null;
  lockoutEnabled?: boolean;
  /** @format int32 */
  accessFailedCount?: number;
  /** @minLength 1 */
  name: string;
  phoneNumber?: string | null;
  email?: string | null;
  role?: Role;
  /** @format int32 */
  animalShelterId?: number | null;
  regularWorkingPlace?: AnimalShelter;
  adminWorkingPlace?: AnimalShelter;
  posts?: FeedPost[] | null;
}

export interface AuthResponseDTO {
  token?: string | null;
  isSucces?: boolean;
  message?: string | null;
}

export interface Category {
  /** @format int32 */
  id?: number;
  name?: string | null;
  urlhandle?: string | null;
  posts?: FeedPost[] | null;
}

export interface CategoryDto {
  /** @format int32 */
  id?: number;
  name?: string | null;
  urlhandle?: string | null;
}

export interface CategoryRequestDto {
  name?: string | null;
  urlhandle?: string | null;
}

export interface CreatePostDTO {
  /** @minLength 1 */
  title: string;
  /** @minLength 1 */
  contentText: string;
  location?: LocationDTO;
  shortAddess?: string | null;
  imageUrl?: string | null;
  /** @format int32 */
  categoryId?: number;
}

export interface CreateRoleDTO {
  /** @minLength 1 */
  roleName: string;
}

export interface FeedPost {
  /** @format int32 */
  id?: number;
  /** @minLength 1 */
  title: string;
  /** @minLength 1 */
  contentText: string;
  /** @format int32 */
  categoryId?: number;
  category?: Category;
  imageUrl?: string | null;
  urlHandle?: string | null;
  /** @format double */
  geoLong?: number;
  /** @format double */
  geoLat?: number;
  address?: string | null;
  /** @format date-time */
  postDate?: string;
  creatorId?: string | null;
  creator?: ApplicationUser;
  associatedAnimals?: Animal[] | null;
}

export interface FeedPostResponseDTO {
  /** @format int32 */
  id?: number;
  title?: string | null;
  contentText?: string | null;
  location?: LocationDTO;
  category?: CategoryDto;
  userID?: string | null;
  imageUrl?: string | null;
}

export interface LocationDTO {
  /** @format double */
  latitude?: number;
  /** @format double */
  longitude?: number;
  address?: string | null;
}

export interface LoginDTO {
  /** @minLength 1 */
  email: string;
  /** @minLength 1 */
  password: string;
}

export interface RegisterDTO {
  /** @minLength 1 */
  name: string;
  /**
   * @format email
   * @minLength 1
   */
  email: string;
  /** @minLength 1 */
  password: string;
  /** @minLength 1 */
  phoneNumber: string;
  roles?: string[] | null;
}

/** @format int32 */
export enum Role {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2,
  Value3 = 3,
  Value4 = 4,
}

export interface RoleDTO {
  id?: string | null;
  roleName?: string | null;
  /** @format int32 */
  userCount?: number;
}

export interface UserDetailDTO {
  id?: string | null;
  name?: string | null;
  email?: string | null;
  roles?: string[] | null;
  phoneNumber?: string | null;
  twoFactorEnabled?: boolean;
  phoneNumberConfirmed?: boolean;
  /** @format int32 */
  accessFailedCount?: number;
}
