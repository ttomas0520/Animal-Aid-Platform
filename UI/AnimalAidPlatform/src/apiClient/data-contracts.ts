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
  notificationSettings?: NotificationSettings;
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
  notificationSettings?: NotificationSettings[] | null;
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

export interface Coordinate {
  /** @format double */
  x?: number;
  /** @format double */
  y?: number;
  /** @format double */
  z?: number;
  /** @format double */
  m?: number;
  coordinateValue?: Coordinate;
  isValid?: boolean;
}

export type CoordinateEqualityComparer = object;

export interface CoordinateSequence {
  /** @format int32 */
  dimension?: number;
  /** @format int32 */
  measures?: number;
  /** @format int32 */
  spatial?: number;
  ordinates?: Ordinates;
  hasZ?: boolean;
  hasM?: boolean;
  /** @format int32 */
  zOrdinateIndex?: number;
  /** @format int32 */
  mOrdinateIndex?: number;
  first?: Coordinate;
  last?: Coordinate;
  /** @format int32 */
  count?: number;
}

export interface CoordinateSequenceFactory {
  ordinates?: Ordinates;
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

/** @format int32 */
export enum Dimension {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2,
  Value3 = 3,
}

export interface Envelope {
  isNull?: boolean;
  /** @format double */
  width?: number;
  /** @format double */
  height?: number;
  /** @format double */
  diameter?: number;
  /** @format double */
  minX?: number;
  /** @format double */
  maxX?: number;
  /** @format double */
  minY?: number;
  /** @format double */
  maxY?: number;
  /** @format double */
  area?: number;
  /** @format double */
  minExtent?: number;
  /** @format double */
  maxExtent?: number;
  centre?: Coordinate;
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
  location?: Point;
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
  creatorName?: string | null;
  imageUrl?: string | null;
}

export interface Geometry {
  factory?: GeometryFactory;
  userData?: any;
  /** @format int32 */
  srid?: number;
  geometryType?: string | null;
  ogcGeometryType?: OgcGeometryType;
  precisionModel?: PrecisionModel;
  coordinate?: Coordinate;
  coordinates?: Coordinate[] | null;
  /** @format int32 */
  numPoints?: number;
  /** @format int32 */
  numGeometries?: number;
  isSimple?: boolean;
  isValid?: boolean;
  isEmpty?: boolean;
  /** @format double */
  area?: number;
  /** @format double */
  length?: number;
  centroid?: Point;
  interiorPoint?: Point;
  pointOnSurface?: Point;
  dimension?: Dimension;
  boundary?: Geometry;
  boundaryDimension?: Dimension;
  envelope?: Geometry;
  envelopeInternal?: Envelope;
  isRectangle?: boolean;
}

export interface GeometryFactory {
  precisionModel?: PrecisionModel;
  coordinateSequenceFactory?: CoordinateSequenceFactory;
  /** @format int32 */
  srid?: number;
  geometryServices?: NtsGeometryServices;
}

export type GeometryOverlay = object;

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

export interface NotificationSettings {
  /** @format int32 */
  id?: number;
  userId?: string | null;
  user?: ApplicationUser;
  pushNotificationEnabled?: boolean;
  /** @format double */
  geoLong?: number;
  /** @format double */
  geoLat?: number;
  address?: string | null;
  location?: Point;
  /** @format double */
  radius?: number;
  categories?: Category[] | null;
}

export interface NotificationSettingsDto {
  pushNotificationEnabled?: boolean;
  location?: LocationDTO;
  /** @format double */
  radius?: number;
  categoryIds?: number[] | null;
}

export interface NtsGeometryServices {
  geometryOverlay?: GeometryOverlay;
  coordinateEqualityComparer?: CoordinateEqualityComparer;
  /** @format int32 */
  defaultSRID?: number;
  defaultCoordinateSequenceFactory?: CoordinateSequenceFactory;
  defaultPrecisionModel?: PrecisionModel;
}

/** @format int32 */
export enum OgcGeometryType {
  Value1 = 1,
  Value2 = 2,
  Value3 = 3,
  Value4 = 4,
  Value5 = 5,
  Value6 = 6,
  Value7 = 7,
  Value8 = 8,
  Value9 = 9,
  Value10 = 10,
  Value11 = 11,
  Value12 = 12,
  Value13 = 13,
  Value14 = 14,
  Value15 = 15,
  Value16 = 16,
}

/** @format int32 */
export enum Ordinates {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2,
  Value3 = 3,
  Value4 = 4,
  Value7 = 7,
  Value8 = 8,
  Value16 = 16,
  Value32 = 32,
  Value64 = 64,
  Value128 = 128,
  Value256 = 256,
  Value512 = 512,
  Value1024 = 1024,
  Value2048 = 2048,
  Value4096 = 4096,
  Value8192 = 8192,
  Value16384 = 16384,
  Value32768 = 32768,
  Value65535 = 65535,
  Value65536 = 65536,
  Value65539 = 65539,
  Value65543 = 65543,
  Value131072 = 131072,
  Value262144 = 262144,
  Value524288 = 524288,
  Value1048576 = 1048576,
  Value2097152 = 2097152,
  Value4194304 = 4194304,
  Value8388608 = 8388608,
  Value16777216 = 16777216,
  Value33554432 = 33554432,
  Value67108864 = 67108864,
  Value134217728 = 134217728,
  Value268435456 = 268435456,
  Value536870912 = 536870912,
  Value1073741824 = 1073741824,
  Value2147483648 = -2147483648,
}

export interface Point {
  factory?: GeometryFactory;
  userData?: any;
  /** @format int32 */
  srid?: number;
  precisionModel?: PrecisionModel;
  /** @format int32 */
  numGeometries?: number;
  isSimple?: boolean;
  isValid?: boolean;
  /** @format double */
  area?: number;
  /** @format double */
  length?: number;
  centroid?: Point;
  interiorPoint?: Point;
  pointOnSurface?: Point;
  envelope?: Geometry;
  envelopeInternal?: Envelope;
  isRectangle?: boolean;
  coordinateSequence?: CoordinateSequence;
  coordinates?: Coordinate[] | null;
  /** @format int32 */
  numPoints?: number;
  isEmpty?: boolean;
  dimension?: Dimension;
  boundaryDimension?: Dimension;
  /** @format double */
  x?: number;
  /** @format double */
  y?: number;
  coordinate?: Coordinate;
  geometryType?: string | null;
  ogcGeometryType?: OgcGeometryType;
  boundary?: Geometry;
  /** @format double */
  z?: number;
  /** @format double */
  m?: number;
}

export interface PrecisionModel {
  isFloating?: boolean;
  /** @format int32 */
  maximumSignificantDigits?: number;
  /** @format double */
  scale?: number;
  /** @format double */
  gridSize?: number;
  precisionModelType?: PrecisionModels;
}

/** @format int32 */
export enum PrecisionModels {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2,
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
